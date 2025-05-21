using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;

[Authorize(Roles = "Patient,Admin")]
public class PatientProfileController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IWebHostEnvironment _environment;

    public PatientProfileController(UserManager<AppUser> userManager,
        ApplicationDbContext context,
        SignInManager<AppUser> signInManager,
        IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return View("DoctorPEdit",user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AppUser model)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return NotFound();

        // تحديث البيانات
        user.FullName = model.FullName;
        user.DateOfBirth = model.DateOfBirth;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;

        // حفظ الصورة
        if (model.imageFile != null)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.imageFile.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.imageFile.CopyToAsync(stream);
            }

            user.ImagePath = fileName;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete()
    {
        var user = await _userManager.GetUserAsync(User);
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == user.Id);

        if (patient != null)
        {
            patient.IsDeleted = true;
            await _context.SaveChangesAsync();

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index");
    }
}
