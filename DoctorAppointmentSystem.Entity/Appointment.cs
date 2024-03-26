using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Entity
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsApproved { get; set; }
        public int PatientId { get; set; }
        public required Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public required Doctor Doctor { get; set; }
    }
}
