using System.ComponentModel.DataAnnotations;

namespace SmartClinic.ViewModels
{
    public class LoginVM
    {


       public string email {  get; set; }

        [DataType(dataType: DataType.Password)]
        [MinLength(10)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
       ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
        public string password { get; set; }



        public bool rememberMe {  get; set; }





    }
}
