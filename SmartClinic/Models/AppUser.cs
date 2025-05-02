using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; } 
        public string? ImagePath { get; set; }
        [NotMapped]

        public IFormFile imageFile { get; set; }
        //public string? Specialization { get; internal set; }
        //public bool IsDeleted { get; internal set; }
    }
}
