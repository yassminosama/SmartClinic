using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime PrescriptionDate { get; set; } 
        public string Diagnoses { get; set;}
        public string Notes { get; set; } 
        public DateTime CreatedAt {  get; set; } 
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public ICollection<Medication> Medications { get; set; }
    }

}
