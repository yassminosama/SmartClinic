using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PrescriptionController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> View()
        {
            var user = await _userManager.GetUserAsync(User);

            var prescriptions = await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Where(p => p.PatientId == user.Id && !p.IsDeleted)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();

            var viewModels = prescriptions.Select(p => new PrescriptionDetailsViewModel
            {
                PrescriptionId = p.PrescriptionId,
                AppointmentId = p.AppointmentId,
                DoctorName = p.Doctor.FullName,
                DoctorSpecialization = p.Doctor.Specialization, 
                PatientName = user.FullName,
                Diagnoses = p.Diagnoses,
                Notes = p.Notes,
                PrescriptionDate = p.PrescriptionDate,
                Medications = p.Medications
            }).ToList();

            return View("DoctorsList", viewModels);
        }

        public async Task<IActionResult> DoctorPrescriptions(string doctorName)
        {
            var user = await _userManager.GetUserAsync(User);

            var prescriptions = await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Where(p => p.PatientId == user.Id && !p.IsDeleted && p.Doctor.FullName == doctorName)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();

            var viewModels = prescriptions.Select(p => new PrescriptionDetailsViewModel
            {
                PrescriptionId = p.PrescriptionId,
                AppointmentId = p.AppointmentId,
                DoctorName = p.Doctor.FullName,
                DoctorSpecialization = p.Doctor.Specialization, 
                PatientName = user.FullName,
                Diagnoses = p.Diagnoses,
                Notes = p.Notes,
                PrescriptionDate = p.PrescriptionDate,
                Medications = p.Medications
            }).ToList();

            ViewBag.DoctorName = doctorName;
            return View("DoctorPrescriptions", viewModels);
        }
    }
}
