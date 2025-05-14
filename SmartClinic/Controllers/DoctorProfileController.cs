using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;
using System.Threading.Tasks;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DoctorProfileController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        // Action method for displaying doctor profile



        public async Task<IActionResult> DoctorProfileDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = user as Doctor;

            if (doctor == null)
                return RedirectToAction("LogIn", "Account");

            // Optionally, load related data if using lazy loading or if needed explicitly
            // e.g., _context.Doctors.Include(d => d.Appointments).ThenInclude(a => a.Patient)... etc.

            return View(doctor);
        }



        [HttpGet]
        public async Task<IActionResult> DoctorPEdit()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = user as Doctor;
            if (doctor == null) return RedirectToAction("Index", "Home");

            var viewModel = new DoctorEditVM
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Email = doctor.Email,
                UserName = doctor.UserName,
                Address = doctor.Address,
                PhoneNumber = doctor.PhoneNumber,
                DateOfBirth = doctor.DateOfBirth,
                Specialization = doctor.Specialization,
                Description = doctor.Description,
                IsAvailable = doctor.IsAvailable,
                ExistingPhotoUrl = doctor.PhotoUrl
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoctorPEdit(DoctorEditVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = user as Doctor;
            if (doctor == null) return RedirectToAction("Index", "Home");

            doctor.FullName = model.FullName;
            doctor.Email = model.Email;
            doctor.UserName = model.UserName;
            doctor.Address = model.Address;
            doctor.PhoneNumber = model.PhoneNumber;
            doctor.DateOfBirth = model.DateOfBirth;
            doctor.Specialization = model.Specialization;
            doctor.Description = model.Description;
            doctor.IsAvailable = model.IsAvailable;

            if (model.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                doctor.PhotoUrl = "/Images/" + uniqueFileName;
            }

            // Optionally update using EF if you have DbContext:
            // _context.Update(doctor);
            // await _context.SaveChangesAsync();

            await _userManager.UpdateAsync(doctor);

            return RedirectToAction("DoctorProfileDetails", "DoctorProfile");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = user as Doctor;

            if (doctor == null)
            {
                return RedirectToAction("LogIn", "Account");
            }

            // Delete the doctor account
            var result = await _userManager.DeleteAsync(doctor);

            if (result.Succeeded)
            {
                await HttpContext.SignOutAsync(); // Log the user out after deletion
                return RedirectToAction("LogOut", "Account");
            }

            // If deletion fails, show errors (optional)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("DoctorProfileDetails");
        }

        public async Task<IActionResult> DoctorAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = user as Doctor;

            if (doctor == null)
                return RedirectToAction("LogIn", "Account");

            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Bill)
                .Where(a => a.DoctorId == doctor.Id && !a.IsDeleted)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            var appointmentViewModels = appointments.Select(a => new AppointmentViewModel
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                PatientId = a.PatientId,
                PatientName = a.Patient != null ? a.Patient.FullName : a.GuestName,
                GuestName = a.GuestName,
                DoctorName = doctor.FullName,
                TimeLeft = (a.AppointmentDate - DateTime.Now).TotalMinutes > 0
                            ? $"{(a.AppointmentDate - DateTime.Now).TotalMinutes:F0} minutes left"
                            : "Passed",
                Status = a.Status,
                BillId = a.Bill?.BillId,
                BillStatus = a.Bill != null ? "Generated" : "Pending"
            }).ToList();

            return View(appointmentViewModels);
        }



        //كود ياسمين


        public async Task<IActionResult> PatientDetails(string patientId)
        {
            if (string.IsNullOrEmpty(patientId)) return NotFound();

            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null) return NotFound();

            var history = await _context.MedicalHistories
                .FirstOrDefaultAsync(h => h.PatientId == patientId && !h.IsDeleted);

            var reports = await _context.Reports
                .Where(r => r.PatientId == patientId && !r.IsDeleted)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();

            var vm = new PatientDetailsVM
            {
                Patient = patient,
                MedicalHistory = history,
                Reports = reports
            };

            return View(vm);
        }


    }
}
