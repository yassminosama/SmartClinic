using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile imageFile { get; set; }
        public string? CreatedByDoctorId { get; set; }

        [ForeignKey("CreatedByDoctorId")]
        public virtual Doctor? CreatedByDoctor { get; set; }
    }
}
