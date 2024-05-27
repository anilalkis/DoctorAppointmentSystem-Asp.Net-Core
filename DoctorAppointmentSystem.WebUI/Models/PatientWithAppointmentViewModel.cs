using DoctorAppointmentSystem.Entity;

namespace DoctorAppointmentSystem.WebUI.Models
{
    public class PatientAppointmentViewModel
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public List<DayOff> daysOff { get; set; }
    }
}
