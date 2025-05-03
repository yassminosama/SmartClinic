using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.ViewModels;

namespace SmartClinic.Controllers
{
    public class AccountController : Controller
    {
       public AccountController(IWebHostEnvironment hosting,UserManager<AppUser> userManager,SignInManager<AppUser> sign) {
            Hosting = hosting;
         
            this.userManager = userManager;
            Sign = sign;
        }

        public IWebHostEnvironment Hosting { get; }
        public ApplicationDbContextFactory Db { get; }
        public UserManager<AppUser> userManager { get; }
        public SignInManager<AppUser> Sign { get; }
        public RoleManager<AppUser> roleManager { get; }

        [HttpGet]
        public IActionResult Register()
        {

            return View();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(registerVM userModel)
        {

           if(ModelState.IsValid) {

                AppUser user;
                if (userModel.Role == "Patient")
                {
                user = new Patient();

                }
                else
                {

                    user = new Doctor();

                }
                   

                    user.FullName = userModel.Name;
                    user.Email = userModel.Email;
                    user.PhoneNumber = userModel.PhoneNumber;
                   

                    user.DateOfBirth = userModel.DateOfBirth;
                    user.Address = userModel.Address;
                    user.UserName = userModel.userName;
                user.Role = userModel.Role;
                    string file;
                    if (userModel.imageFile != null)
                    {
                        file = Path.Combine(Hosting.WebRootPath, "Images");

                        string FullPath = Path.Combine(file, userModel.imageFile.Name);
                        user.ImagePath = userModel.imageFile.Name;
                        userModel.imageFile.CopyTo(new FileStream(FullPath, FileMode.Create));



                    }


                    IdentityResult res = await userManager.CreateAsync(user,userModel.PassWord);
                    if (res.Succeeded)
                    {

                        await userManager.AddToRoleAsync(user, userModel.Role);
                    }
                  

               


                   


                

             

             






            }

  return RedirectToAction("Home/Index");


        }



        [HttpGet]
        public IActionResult logIn()
        {



            return View();

        }


        [HttpPost]
        public async Task<IActionResult> logIn(LoginVM userModel)
        {

            if (ModelState.IsValid)
            {

                AppUser user = await userManager.FindByEmailAsync(userModel.email);


                if (user != null)
                {

                    if (await userManager.CheckPasswordAsync(user,userModel.password))
                    {
                     await   Sign.SignInAsync(user, userModel.rememberMe);

                    }
                 



                }
           





            }
          
            return View(userModel);

        }

        [HttpGet]
        public IActionResult logOut()
        {


            Sign.SignOutAsync();
            return RedirectToAction("logIn");




        }
       
    }
}
