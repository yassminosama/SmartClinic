using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.ViewModels
{
    public class registerVM
    {
        [Required]
        public string Name { get; set; }

<<<<<<< HEAD
        public string? UserId {  get; set; }
        public string Name { get; set; } 
        public string userName { get; set; }
        [DataType(dataType:DataType.EmailAddress)]
=======
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
>>>>>>> ae84cc30f4b0777f705b7e74ccee26b8864416da
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 50 characters.")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(10)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
        public string PassWord { get; set; }

        [Required]
        [Compare("PassWord", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassWord { get; set; }

        [Required]
        public string Role { get; set; }

        public string Gender { get; set; }
        public IFormFile imageFile { get; set; }

        public string? Specialization { get; set; }

        public decimal? Salary { get; set; }

        public List<DateTime?> ExceptionDates { get; set; } = new List<DateTime?>();
        public List<string?> DefaultDate { get; set; } = new List<string>();
    }
}
