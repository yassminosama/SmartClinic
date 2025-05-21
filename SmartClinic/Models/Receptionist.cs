using System.ComponentModel.DataAnnotations.Schema;  // This provides the [ForeignKey] attribute
namespace SmartClinic.Models
{
    public class Receptionist : AppUser
    {
        public decimal? Salary { get; set; }

        public bool IsDeleted { get; set; } = false; 

        // Relationship to the doctor they work with
        public string? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor? Doctor { get; set; }

        // Navigation properties
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}