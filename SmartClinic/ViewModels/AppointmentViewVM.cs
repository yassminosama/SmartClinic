namespace SmartClinic.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string GuestName { get; set; }
        public string DoctorName { get; set; }
        public string TimeLeft { get; set; }
        public string Status { get; set; }
        public string BillStatus { get; set; }
        public int? BillId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}