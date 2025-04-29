using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Patient : IdentityUser
    {
        [Key]
            public int PatientId { get; set; }
            public string FullName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Image { get; set; }
            public bool IsDeleted { get; set; } = false;

            // Navigation properties
            public ICollection<Appointment> Appointments { get; set; }
            public ICollection<Bill> Bills { get; set; }
            public ICollection<Report> Reports { get; set; }
            public ICollection<Prescription> Prescriptions { get; set; }
            public ICollection<MedicalHistory> MedicalHistories { get; set; }
        }

    }

