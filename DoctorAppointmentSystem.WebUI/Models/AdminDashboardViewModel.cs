using DoctorAppointmentSystem.Entity;

namespace DoctorAppointmentSystem.WebUI.Models
{
    public class AdminDashboardViewModel
    {
        public List<Doctor> doctors { get; set; }
        public List<Patient> patients { get; set; }
        public List<Appointment> appointments { get; set; }
        public List<MonthlyAppointmentCount> monthlyAppointmentCounts { get; set; }
        public YearlyAppointmentCount yearlyAppointmentCount { get; set; }
        public List<MonthlyPatientCount> monthlyPatientCounts { get; set; }
        public YearlyPatientCount yearlyPatientCount { get; set; }
        public int dayOffs { get; set; }

    }
}
