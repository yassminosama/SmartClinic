using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    public class Patient : AppUser
    {
       
           
          
          
          
          
           
         

            public bool IsDeleted { get; set; } = false;

            // Navigation properties
            public ICollection<Appointment> Appointments { get; set; }
            public ICollection<Bill> Bills { get; set; }
            public ICollection<Report> Reports { get; set; }
            public ICollection<Prescription> Prescriptions { get; set; }
            public ICollection<MedicalHistory> MedicalHistories { get; set; }
        }

    }

