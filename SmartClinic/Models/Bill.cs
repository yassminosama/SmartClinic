using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public string PatientId { get; set; }
        public string ReceptionistId { get; set; }
        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPayed { get; set; }
        public bool IsRefunded { get; set; }
 

        // Navigation properties
        public Patient Patient { get; set; }
        public Receptionist Receptionist { get; set; }
        public Appointment Appointment { get; set; }
    }

}
