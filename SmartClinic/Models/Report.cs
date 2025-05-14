using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        public string? PatientId { get; set; }

        public DateTime ReportDate { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Attachment { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}

