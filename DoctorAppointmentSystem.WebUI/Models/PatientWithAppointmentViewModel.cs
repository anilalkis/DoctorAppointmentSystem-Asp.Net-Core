using DoctorAppointmentSystem.Entity;

namespace DoctorAppointmentSystem.WebUI.Models
{
    public class PatientWithAppointmentViewModel
    {
        public Patient Patient { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
