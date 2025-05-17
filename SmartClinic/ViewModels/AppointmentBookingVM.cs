//using System.ComponentModel.DataAnnotations;

//namespace SmartClinic.ViewModels
//{
//    public class AppointmentBookingVM
//    {
//        [Required]
//        public string SelectedSpecialization { get; set; }

//        [Required]
//        public string DoctorId { get; set; }

//        [Required]
//        [DataType(DataType.Date)]
//        public DateTime AppointmentDate { get; set; }

//        public List<string> Specializations { get; set; } = new List<string>();
//    }
//}


using System.ComponentModel.DataAnnotations;
namespace SmartClinic.ViewModels
{
    public class AppointmentBookingVM
    {
    public List<string> Specializations { get; set; } = new List<string>();
    [Required]
    public string SelectedSpecialization { get; set; }

    [Required]
    public string DoctorId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime AppointmentDate { get; set; }
    }
}
