using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartClinic.Models;
using SmartClinic.ViewModels;
using System.Threading.Tasks;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public DoctorProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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


    }
}
