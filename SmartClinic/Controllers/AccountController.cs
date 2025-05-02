using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartClinic.Models;
using SmartClinic.ViewModels;

namespace SmartClinic.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _hosting;

        public AccountController(IWebHostEnvironment hosting, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _hosting = hosting;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(registerVM userModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user;

                if (userModel.Role == "Doctor")
                {
                    user = new Doctor
                    {
                        FullName = userModel.Name,
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        DateOfBirth = userModel.DateOfBirth,
                        Address = userModel.Address,
                        UserName = userModel.UserName,
                        Specialization = userModel.Specialization,
                        IsAvailable = true,
                        IsDeleted = false
                    };
                }
                else if (userModel.Role == "Receptionist")
                {
                    user = new Receptionist
                    {
                        FullName = userModel.Name,
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        DateOfBirth = userModel.DateOfBirth,
                        Address = userModel.Address,
                        UserName = userModel.UserName,
                        IsDeleted = false,
                        Salary = userModel.Salary
                    };
                }
                else // Default to Patient if no role is specified
                {
                    user = new Patient
                    {
                        FullName = userModel.Name,
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        DateOfBirth = userModel.DateOfBirth,
                        Address = userModel.Address,
                        UserName = userModel.UserName,
                        IsDeleted = false
                    };
                }

                if (userModel.imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_hosting.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(userModel.imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userModel.imageFile.CopyToAsync(stream);
                    }

                    user.ImagePath = uniqueFileName;
                }

                var result = await _userManager.CreateAsync(user, userModel.PassWord);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userModel.Role);
                    TempData["Success"] = "User registered successfully.";
                    return RedirectToAction("LogIn");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(userModel);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginVM userModel)
        {
            if (!ModelState.IsValid)
                return View(userModel);

            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    userModel.Password,
                    userModel.RememberMe,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, "Patient"))
                        return RedirectToAction("Index", "PatientDashboard");

                    if (await _userManager.IsInRoleAsync(user, "Doctor"))
                        return RedirectToAction("Index", "DoctorDashboard");

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }
    }
}
