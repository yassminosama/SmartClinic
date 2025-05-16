using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;

namespace SmartClinic.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {


       public AdminController(IWebHostEnvironment Host , UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,ApplicationDbContext db)
        {
            this.Host = Host;
            this.roleManager = roleManager;
            Db = db;
            this.userManager = userManager;
        }

        public IWebHostEnvironment Host { get; }
        public RoleManager<IdentityRole> roleManager { get; }
        public ApplicationDbContext Db { get; }
        public UserManager<AppUser> userManager { get; }



        public IActionResult Index()
        {
            var totalDoctors = Db.Doctors.Count();
            var totalPatients = Db.Patients.Count();
            var totalReceptionists = Db.Receptionists.Count();
            var totalAdmins = Db.Users.Count(u => u.Role == "Admin");

            var model = new AdminDashboardVM
            {
                DoctorCount = totalDoctors,
                PatientCount = totalPatients,
                ReceptionistCount = totalReceptionists,
                AdminCount = totalAdmins
            };

            return View(model);
        }




        [HttpGet]
        public IActionResult addAdmin()
        {

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> addAdmin(AdminVM adminUser)
        {


            if (ModelState.IsValid)
            {


                AppUser user = new AppUser();

                user.Address = adminUser.Address;
                user.FullName = adminUser.Name;
                user.Email = adminUser.Email;
                user.PhoneNumber = adminUser.PhoneNumber;
                user.Gender = adminUser.Gender;
                user.DateOfBirth = adminUser.DateOfBirth;
                user.UserName = adminUser.userName;
                user.Role = "Admin";
                string filePath;
                if (adminUser.imageFile != null)
                {

                    string file = Path.Combine(Host.WebRootPath, "Images");
                    filePath = Path.Combine(file, adminUser.imageFile.FileName);
                    user.ImagePath = adminUser.imageFile.FileName;
                    adminUser.imageFile.CopyTo(new FileStream(filePath, FileMode.Create));




                }
                IdentityResult res = await userManager.CreateAsync(user, adminUser.PassWord);
                if (res.Succeeded)
                {


                    await userManager.AddToRoleAsync(user, "Admin");

                }





            }



            return View();


        }
        [HttpGet]
        public IActionResult createRole() {



            return View();
        
        
        }


        [HttpPost]
        public async Task< IActionResult >createRole(RoleVM roleModel)
        {
            if (ModelState.IsValid)
            {

               IdentityRole role = new IdentityRole();
                role.Name=roleModel.name;

                IdentityResult res=await roleManager.CreateAsync(role);


            }


            return View();


        }


    

        public IActionResult showPatients()
        {

            List<Patient> patients=Db.Patients.ToList();

            //  return View("showPatients",patients);


            return View(patients);
        }



        [HttpGet]
        public IActionResult showDoctors()
        {

            List<Doctor> doctors = Db.Doctors.ToList();

            return View(doctors);



        }

        public IActionResult showAdmins()
        {

            List<AppUser> admins = Db.Users.Where(u=>u.Role=="Admin").ToList();

            return View(admins);



        }










    }
}
