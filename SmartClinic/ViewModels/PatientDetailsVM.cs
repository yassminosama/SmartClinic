﻿using SmartClinic.Models;
using System.Collections.Generic;

namespace SmartClinic.ViewModels
{
    public class PatientDetailsVM
    {
        public Patient Patient { get; set; }
        public List<Report> Reports { get; set; }
        public MedicalHistory MedicalHistory { get; set; }
        public List<Prescription> Prescriptions { get; set; } 
        public string CurrentDoctorId { get; set; }
    }
}
