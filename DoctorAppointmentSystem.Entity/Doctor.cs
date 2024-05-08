using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Entity
{
    public class Doctor
    {
        public Doctor()
        {
            Schedules = new List<Schedule>();
            Appointments = new List<Appointment>();
        }
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? Specialization { get; set; }
        public string? Picture { get; set; }
        public string? Bio { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
