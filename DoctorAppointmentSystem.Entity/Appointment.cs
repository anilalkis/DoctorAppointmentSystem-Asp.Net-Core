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
        public State IsApproved { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }

    }

    public class MonthlyAppointmentCount
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
    }

    public class YearlyAppointmentCount 
    {
        public int ThisYear { get; set;}
        public int LastYear { get; set; }
    }

}
