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
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var today = DateTime.Today;
            var appointments = await _context.Appointments
                .Where(a => !a.IsDeleted && a.AppointmentDate.Date == today)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Bill)
                .OrderBy(a => a.CreatedAt) // Order by creation time
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
                .Where(d => !d.IsDeleted)
                .ToListAsync();

            var viewModel = new ReceptionistDashboardVM
            {
                User = user,
                Appointments = appointments,
                Doctors = doctors
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGuestAppointment(string guestName, string guestPhone, string doctorId, DateTime appointmentDate)
        {
            if (string.IsNullOrEmpty(guestName) || string.IsNullOrEmpty(guestPhone) || string.IsNullOrEmpty(doctorId))
            {
                return BadRequest("All fields are required.");
            }

            var appointment = new Appointment
            {
                GuestName = guestName,
                GuestPhone = guestPhone,
                DoctorId = doctorId,
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