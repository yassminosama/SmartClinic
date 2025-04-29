using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Doctor : IdentityUser
    {
            [Key]
            public int DoctorId { get; set; }
            public string FullName { get; set; }
            public string Specialization { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Image { get; set; }
            public List<DateTime?> ExceptionDates { get; set; } = new List<DateTime?>();
            public List<string> DefaultDate { get; set; } = new List<string>();
            public bool IsAvailable { get; set; } = true;
            public bool IsDeleted { get; set; } = false;

            // Navigation properties
            public ICollection<Appointment> Appointments { get; set; }
            public ICollection<Prescription> Prescriptions { get; set; }
        }

    }

