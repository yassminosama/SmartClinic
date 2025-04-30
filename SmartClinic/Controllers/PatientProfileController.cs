using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartClinic.Data;
using SmartClinic.Models;
using System.Linq;


/*public class PatientProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatientProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: /PatientProfile/
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var patient = _context.Patients.FirstOrDefault(p => p.UserId == user.Id);

        if (patient == null)
            return NotFound();

        return View(patient);
    }

    // GET: /PatientProfile/Edit
    public async Task<IActionResult> Edit()
    {
        var user = await _userManager.GetUserAsync(User);
        var patient = _context.Patients.FirstOrDefault(p => p.UserId == user.Id);

        if (patient == null)
            return NotFound();

        return View(patient);
    }

    // POST: /PatientProfile/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Patient updatedPatient)
    {
        if (!ModelState.IsValid)
            return View(updatedPatient);

        var user = await _userManager.GetUserAsync(User);
        var patient = _context.Patients.FirstOrDefault(p => p.UserId == user.Id);

        if (patient == null)
            return NotFound();

        patient.Full_Name = updatedPatient.Full_Name;
        patient.Phone = updatedPatient.Phone;
        patient.Address = updatedPatient.Address;
        patient.Image = updatedPatient.Image;
        patient.Gender = updatedPatient.Gender;
        patient.Date_of_birth = updatedPatient.Date_of_birth;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}


*/