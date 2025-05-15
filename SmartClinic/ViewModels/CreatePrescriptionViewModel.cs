namespace SmartClinic.ViewModels
{
    public class CreatePrescriptionViewModel
    {
        public int AppointmentId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string Diagnoses { get; set; }
        public string Notes { get; set; }

        public List<MedicationViewModel> Medications { get; set; }
    }

    public class MedicationViewModel
    {
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Instructions { get; set; }
    }
}