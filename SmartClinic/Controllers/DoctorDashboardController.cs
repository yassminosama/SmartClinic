using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartClinic.Models;

namespace SmartClinic.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public DoctorDashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> DashDIndex()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = user as Doctor;
            return View(doctor);
        }

    }
}