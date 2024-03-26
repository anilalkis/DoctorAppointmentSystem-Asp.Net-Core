﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Entity
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
        }
        public int PatientId { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}