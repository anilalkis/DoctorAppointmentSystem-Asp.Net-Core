using DoctorAppointmentSystem.Entity;

namespace DoctorAppointmentSystem.WebUI.Models
{
    public class AdminDashboardViewModel
    {
        public List<Doctor> doctors { get; set; }
        public List<Patient> patients { get; set; }
        public List<Appointment> appointments { get; set; }
    }
}
