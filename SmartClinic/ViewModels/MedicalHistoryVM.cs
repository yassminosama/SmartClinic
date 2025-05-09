namespace SmartClinic.ViewModels
{

    public class MedicalHistoryVM
    {
        public List<DiagnosisEntry> Diagnoses { get; set; } = new List<DiagnosisEntry>();
    }
    public class DiagnosisEntry
    {
        public DateTime? Date { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
    }




}
