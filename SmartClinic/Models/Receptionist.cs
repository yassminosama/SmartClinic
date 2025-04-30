using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
        public class Receptionist : AppUser
    {
    
     
            public string? FullName { get; set; }
         
         
            public string Image { get; set; }
            public decimal? Salary { get; set; }
            public bool IsDeleted { get; set; } = false;

            // Navigation properties
            public ICollection<Bill> Bills { get; set; }
            public ICollection<Appointment> Appointments { get; set; }
        public string? DoctorId { get; set; }  // Foreign Key
        public Doctor? Doctor { get; set; }  // Navigation Property
    }

    }

