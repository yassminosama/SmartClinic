using System.ComponentModel.DataAnnotations;

namespace SmartClinic.ViewModels
{
    public class AdminVM
 
        {


            public string Name { get; set; }
            public string userName { get; set; }
            [DataType(dataType: DataType.EmailAddress)]
            public string Email { get; set; }

            [MinLength(3, ErrorMessage = "address number should be between 3 digits and 50 digits"),]
            [MaxLength(50)]
            public string Address { get; set; }
            [DataType(dataType: DataType.Date)]
            public DateTime? DateOfBirth { get; set; }
            [DataType(dataType: DataType.PhoneNumber)]
            [MinLength(11, ErrorMessage = "phone number should be 11 digits"),]
            [MaxLength(11)]

            public string PhoneNumber { get; set; }
            [DataType(dataType: DataType.Password)]
            [MinLength(10)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
            public string PassWord { get; set; }

            [Compare("PassWord")]
            [DataType(dataType: DataType.Password)]
            public string ConfirmPassWord { get; set; }

         
        public string Gender {  get; set; } 

            public IFormFile imageFile { get; set; }








        
    }

}

