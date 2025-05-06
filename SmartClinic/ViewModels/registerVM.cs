using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.ViewModels
{
    public class registerVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
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

        public IFormFile imageFile { get; set; }

        public string? Specialization { get; set; }

        public decimal? Salary { get; set; }
        public string? Description { get; set; }

        public List<DateTime?> ExceptionDates { get; set; } = new List<DateTime?>();
        public List<string?> DefaultDate { get; set; } = new List<string>();
    }
}
