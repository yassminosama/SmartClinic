using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        public int PrescriptionId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Instructions { get; set; }

        // Navigation property
        public Prescription Prescription { get; set; }
    }

}
