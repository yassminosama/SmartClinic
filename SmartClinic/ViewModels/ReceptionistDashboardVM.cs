using SmartClinic.Models;
using System.Collections.Generic;

namespace SmartClinic.ViewModels
{
    public class ReceptionistDashboardVM
    {
        public AppUser User { get; set; }
        public List<AppointmentViewModel> Appointments { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}