
namespace SmartClinic.ViewModels
{
    public class PatientAppointmentVM
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public bool IsPayed { get; set; }
        public decimal Amount { get; set; }
    }
}
