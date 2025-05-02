using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Doctor : AppUser
    {
            

            public string ?Specialization { get; set; }
       
       


         
            public List<DateTime?> ExceptionDates { get; set; } = new List<DateTime?>();
            public List<string> DefaultDate { get; set; } = new List<string>();
            public bool IsAvailable { get; set; } = true;
            public bool IsDeleted { get; set; } = false;

            // Navigation properties
            public ICollection<Appointment> Appointments { get; set; }
            public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Receptionist> Receptionists { get; set; }
    }

    }

