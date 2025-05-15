using SmartClinic.Models;

namespace SmartClinic.ViewModels
{
    public class PrescriptionDetailsViewModel
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Diagnoses { get; set; }
        public string Notes { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public IEnumerable<Medication> Medications { get; set; }  // Ensure this is a collection
    }


}
