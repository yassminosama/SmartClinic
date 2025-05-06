using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartClinic.Models;
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
        public async Task<IActionResult> DoctorProfileIndex()
        {
            // Get the logged-in user (doctor)
            var doctor = await _userManager.GetUserAsync(User);

            if (doctor == null)
            {
                // Handle error or redirect if doctor not found
                return RedirectToAction("Index", "Home");
            }

            // Cast the user to a Doctor type (if you have a Doctor class)
            var doctorProfile = doctor as Doctor;

            // Return the view and pass the doctorProfile to the view
            return View(doctorProfile);
        }


        [HttpGet]
        public async Task<IActionResult> DoctorPEdit()
        {
            var doctor = await _userManager.GetUserAsync(User);
            if (doctor == null) return RedirectToAction("Index", "Home");

            return View(doctor);
        }



        [HttpPost]
        public async Task<IActionResult> DoctorPEdit(Doctor updatedDoctor)
        {
            var doctor = await _userManager.GetUserAsync(User);
            if (doctor == null) return RedirectToAction("Index", "Home");

            var profile = doctor as Doctor;
            if (profile == null) return BadRequest();

            profile.UserName = updatedDoctor.UserName;
            profile.Email = updatedDoctor.Email;
            profile.Specialization = updatedDoctor.Specialization;
            profile.IsAvailable = updatedDoctor.IsAvailable;

            // If you are using DbContext
            // _context.Update(profile);
            // await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}
