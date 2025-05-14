using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;
using System.Security.Claims;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Patient")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ReportController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Upload()
        {
            return View(new ReportUploadVM());
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ReportUploadVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "reports");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var report = new Report
            {
                PatientId = patientId,
                ReportDate = DateTime.Now,
                Description = model.Description,
                Attachment = "/uploads/reports/" + fileName
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyReports");
        }

        public async Task<IActionResult> MyReports()
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reports = await _context.Reports
                .Where(r => r.PatientId == patientId && !r.IsDeleted)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();

            return View(reports);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();

            report.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("MyReports");
        }
    }
}
