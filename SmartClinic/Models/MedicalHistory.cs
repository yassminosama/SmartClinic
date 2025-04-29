using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class MedicalHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public List<string> Diagnoses { get; set; } = new List<string>();
        public DateTime? DiagnosesDate { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
        public bool IsDeleted { get; set; } = false;

        // Navigation property
        public Patient Patient { get; set; }
    }

}
