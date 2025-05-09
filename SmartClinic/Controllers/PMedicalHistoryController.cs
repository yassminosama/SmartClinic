using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data; 
using SmartClinic.Models;
using SmartClinic.ViewModels;

namespace SmartClinic.Controllers
{
    public class PMedicalHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> userManager;

        public PMedicalHistoryController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> CreateOrEdit()
        {
            var patient = await userManager.GetUserAsync(User) as Patient;

            var existing = await _context.MedicalHistories
                .FirstOrDefaultAsync(h => h.PatientId == patient.Id && !h.IsDeleted);

            var model = new MedicalHistoryVM();

            if (existing != null)
            {
                for (int i = 0; i < existing.Diagnoses.Count; i++)
                {
                    model.Diagnoses.Add(new DiagnosisEntry
                    {
                        Date = existing.DiagnosesDate,
                        Diagnosis = existing.Diagnoses[i],
                        Notes = existing.Notes.ElementAtOrDefault(i)
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMedicalHistory(MedicalHistoryVM model)
        {
            var patient = await userManager.GetUserAsync(User) as Patient;

            var history = await _context.MedicalHistories
                .FirstOrDefaultAsync(h => h.PatientId == patient.Id && !h.IsDeleted);

            if (history == null)
            {
                history = new MedicalHistory
                {
                    PatientId = patient.Id
                };
                _context.MedicalHistories.Add(history);
            }

            history.Diagnoses = model.Diagnoses.Select(d => d.Diagnosis).ToList();
            history.Notes = model.Diagnoses.Select(d => d.Notes).ToList();
            history.DiagnosesDate = model.Diagnoses.FirstOrDefault()?.Date;

            await _context.SaveChangesAsync();
            return RedirectToAction("Details");
        }


        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var patient = await userManager.GetUserAsync(User) as AppUser;

            var history = await _context.MedicalHistories
                .FirstOrDefaultAsync(h => h.PatientId == patient.Id && !h.IsDeleted);

            if (history == null)
            {
                return RedirectToAction("CreateOrEdit");
            }

            var model = new MedicalHistoryVM();

            for (int i = 0; i < history.Diagnoses.Count; i++)
            {
                model.Diagnoses.Add(new DiagnosisEntry
                {
                    Date = history.DiagnosesDate,
                    Diagnosis = history.Diagnoses[i],
                    Notes = history.Notes.ElementAtOrDefault(i)
                });
            }

            return View(model);
        }

    }
}
