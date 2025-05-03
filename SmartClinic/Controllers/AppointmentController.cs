using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;

public class AppointmentController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> userManager;

    public AppointmentController(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Book()
    {
        var model = new AppointmentBookingVM
        {
            Specializations = GetDoctorSpecializations()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Book(AppointmentBookingVM model)
    {
        if (!ModelState.IsValid)
        {
            model.Specializations = GetDoctorSpecializations();
            return View(model);
        }

        var doctorRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Doctor")?.Id;

        var doctorIds = _context.UserRoles
            .Where(ur => ur.RoleId == doctorRoleId)
            .Select(ur => ur.UserId)
            .ToList();

        var doctor = await _context.Users
            .OfType<Doctor>()
            .Include(d => d.Appointments)
            .FirstOrDefaultAsync(d => doctorIds.Contains(d.Id) && d.Id == model.DoctorId && !d.IsDeleted);

        if (doctor == null)
        {
            ModelState.AddModelError("", "Doctor not found.");
            model.Specializations = GetDoctorSpecializations();
            return View(model);
        }

        var appointmentDay = model.AppointmentDate.DayOfWeek.ToString();

        bool isExceptionDate = doctor.ExceptionDates
            .Any(ed => ed.HasValue && ed.Value.Date == model.AppointmentDate.Date);

        // Check if the doctor is not available due to default off days
        bool isWeeklyOff = doctor.DefaultDate.Contains(appointmentDay);

        if (isExceptionDate || isWeeklyOff)
        {
            ModelState.AddModelError("", "The doctor is not available on the selected date.");
            model.Specializations = GetDoctorSpecializations();
            return View(model);
        }

        var patient = await userManager.GetUserAsync(User) as Patient;
        if (patient == null)
        {
            ModelState.AddModelError("", "Patient not found.");
            model.Specializations = GetDoctorSpecializations();
            return View(model);
        }

        var appointment = new Appointment
        {
            DoctorId = doctor.Id,
            PatientId = patient.Id,
            AppointmentDate = model.AppointmentDate,
            CreatedAt = DateTime.Now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        var bill = new Bill
        {
            AppointmentId = appointment.AppointmentId,
            PatientId = patient.Id,
            Amount = 200,
            BillDate = DateTime.Now,
            PaymentDate = DateTime.Now,
            IsPayed = false,
            IsRefunded = false
        };

        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();

        //TempData["Success"] = "Appointment booked successfully.";
        //return RedirectToAction("Book");

        TempData["Success"] = "Appointment booked successfully.";
        return RedirectToAction("MyAppointments"); 

    }

    [HttpGet]
    public JsonResult GetDoctorsBySpecialization(string specialization)
    {
        var doctorRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Doctor")?.Id;

        if (doctorRoleId == null)
            return Json(new { success = false, message = "Doctor role not found" });

        var doctorIds = _context.UserRoles
            .Where(ur => ur.RoleId == doctorRoleId)
            .Select(ur => ur.UserId)
            .ToList();

        var doctors = _context.Doctors
            .Where(u => doctorIds.Contains(u.Id)
                        && u.Specialization == specialization
                        && !u.IsDeleted)
            .Select(u => new { u.Id, u.FullName })
            .ToList();

        return Json(doctors);
    }

    [HttpGet]
    public JsonResult GetDoctorAvailability(string doctorId)
    {
        var doctor = _context.Doctors
        .FirstOrDefault(d => d.Id == doctorId);


        if (doctor == null)
            return Json(new { success = false, message = "Doctor not found" });

        // Get Default (Days Off) and Exception Dates
        var daysOff = doctor.DefaultDate ?? new List<string>();
        var exceptions = doctor.ExceptionDates
            .Where(e => e.HasValue)
            .Select(e => e.Value.ToString("yyyy-MM-dd"))
            .ToList();

        return Json(new { daysOff, exceptions });
    }

    private List<string> GetDoctorSpecializations()
    {
        var doctorRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Doctor")?.Id;

        var doctorIds = _context.UserRoles
            .Where(ur => ur.RoleId == doctorRoleId)
            .Select(ur => ur.UserId)
            .ToList();

        var specializations = _context.Doctors
            .Where(u => doctorIds.Contains(u.Id))
            .Select(u => u.Specialization)
            .Where(s => !string.IsNullOrEmpty(s))
            .Distinct()
            .ToList();

        return specializations;
    }

    //بتعرض الحجوزات للمريض
    [HttpGet]
    public async Task<IActionResult> MyAppointments()
    {
        var patient = await userManager.GetUserAsync(User) as Patient;
        if (patient == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var appointments = await _context.Appointments
            .Where(a => a.PatientId == patient.Id)
            .Include(a => a.Doctor)
            .Include(a => a.Bill)
            .Select(a => new PatientAppointmentVM
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                DoctorName = a.Doctor.FullName,
                Specialization = a.Doctor.Specialization,
                IsPayed = a.Bill != null && a.Bill.IsPayed,
                Amount = a.Bill != null ? a.Bill.Amount : 0
            })
            .ToListAsync();

        return View(appointments);
    }




}
