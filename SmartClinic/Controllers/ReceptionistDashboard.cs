using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistDashboard : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ReceptionistDashboard(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> DashRIndex()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Error", "Home");

            // Get the creating doctor's ID (use CreatedByDoctorId instead of DoctorId)
            var createdByDoctorId = user.CreatedByDoctorId;
            if (string.IsNullOrEmpty(createdByDoctorId))
            {
                return View(new ReceptionistDashboardVM
                {
                    User = user,
                    Appointments = new List<AppointmentViewModel>(),
                    Doctors = new List<Doctor>()
                });
            }

            var today = DateTime.Today;
            var appointments = await _context.Appointments
                .Where(a => !a.IsDeleted &&
                       a.AppointmentDate.Date == today &&
                       a.DoctorId == createdByDoctorId) // Filter by creating doctor
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Bill)
                .OrderBy(a => a.CreatedAt)
                .Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.AppointmentId,
                    PatientId = a.PatientId,
                    PatientName = a.PatientId == null ? null : a.Patient.FullName,
                    GuestName = a.GuestName,
                    DoctorName = a.Doctor.FullName,
                    TimeLeft = (a.AppointmentDate - DateTime.Now).TotalSeconds > 0
                        ? (a.AppointmentDate - DateTime.Now).ToString(@"hh\:mm\:ss")
                        : "00:00:00",
                    Status = a.Status,
                    BillStatus = a.GuestName != null ? "Paid" : (a.Bill != null && a.Bill.IsPayed ? "Paid" : "Not Paid"),
                    BillId = a.Bill != null ? a.Bill.BillId : (int?)null,
                    AppointmentDate = a.AppointmentDate,
                    CreatedAt = a.CreatedAt // Added to show creation time
                })
                .ToListAsync();

            var doctors = await _context.Doctors
        .Where(d => !d.IsDeleted && d.Id == createdByDoctorId)
        .ToListAsync();

            return View(new ReceptionistDashboardVM
            {
                User = user,
                Appointments = appointments,
                Doctors = doctors
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGuestAppointment(string guestName, string guestPhone, DateTime appointmentDate)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Error", "Home");

            if (string.IsNullOrEmpty(guestName) || string.IsNullOrEmpty(guestPhone))
            {
                return BadRequest("Guest name and phone are required.");
            }

            // Automatically use the creating doctor's ID
            var createdByDoctorId = user.CreatedByDoctorId;
            if (string.IsNullOrEmpty(createdByDoctorId))
            {
                return BadRequest("No associated doctor found.");
            }

            var appointment = new Appointment
            {
                GuestName = guestName,
                GuestPhone = guestPhone,
                DoctorId = createdByDoctorId, // Auto-assign to creating doctor
                AppointmentDate = appointmentDate,
                Status = "Scheduled",
                CreatedAt = DateTime.Now
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction("DashRIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsEntered(int appointmentId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            appointment.Status = "Entered";
            await _context.SaveChangesAsync();

            return RedirectToAction("DashRIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkBillAsPaid(int billId)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(b => b.BillId == billId);
            if (bill == null)
            {
                return NotFound("Bill not found.");
            }

            bill.IsPayed = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("DashRIndex");
        }
    }
}