using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.ViewModels
{
    public class ReportUploadVM
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "File is required.")]
        [Display(Name = "Upload File")]
        public IFormFile File { get; set; }
    }
}
