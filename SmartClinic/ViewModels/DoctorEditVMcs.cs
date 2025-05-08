using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.ViewModels
{
    public class DoctorEditVM
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string? Specialization { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? ExistingPhotoUrl { get; set; }
    }
}
