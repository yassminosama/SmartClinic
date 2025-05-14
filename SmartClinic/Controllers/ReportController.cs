using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Patient")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ReportController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Report model, IFormFile ReportFile)
        {
            if (!ModelState.IsValid || ReportFile == null)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == user.Id);

            if (patient == null) return NotFound();

            // Save file to wwwroot/reports
            var uploadsFolder = Path.Combine(_env.WebRootPath, "reports");
            Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

            var fileName = Guid.NewGuid() + Path.GetExtension(ReportFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ReportFile.CopyToAsync(fileStream);
            }

            model.Attachment = fileName;
            model.PatientId = patient.Id;
            model.ReportDate = DateTime.Now;

            _context.Reports.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyReports");
        }

        [HttpGet]
        public async Task<IActionResult> MyReports()
        {
            var user = await _userManager.GetUserAsync(User);
            var reports = await _context.Reports
                .Where(r => r.PatientId == user.Id && !r.IsDeleted)
                .ToListAsync();

            return View(reports);
        }
    }
}
