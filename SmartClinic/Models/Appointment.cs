using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Scheduled";
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public Bill Bill { get; set; }
    }

}
