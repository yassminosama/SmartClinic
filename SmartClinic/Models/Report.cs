using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public string? PatientId { get; set; }
        public DateTime ReportDate { get; set; } 
        public string Description { get; set; }
        public string Attachment { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public Patient Patient { get; set; }

    }

}
