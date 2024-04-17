using DoctorAppointmentSystem.WebUI.Models;
using DoctorAppointmentSystem.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DoctorAppointmentSystem.Data.Abstract;
using Microsoft.AspNetCore.Components;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private IPatientDal _patientDal;
        private IAppointmentDal _appointmentDal;
        private IDoctorDal _doctorDal;
        private IScheduleDal _scheduleDal;

        public PatientController(ILogger<PatientController> logger, IPatientDal patientDal, IAppointmentDal appointmentDal, IDoctorDal doctorDal, IScheduleDal scheduleDal)
        {
            _logger = logger;
            _patientDal = patientDal;
            _appointmentDal = appointmentDal;
            _doctorDal = doctorDal;
            _scheduleDal = scheduleDal;

        }

        public IActionResult Index()
        {

            PatientWithAppointmentViewModel patient = new PatientWithAppointmentViewModel();
            patient.Patient = _patientDal.GetById(1);
            patient.Appointments = _appointmentDal.GetAppointmentsWithPatientId(1);

            return View(patient);
        }

        public IActionResult DoctorsList()
        {
            List<Doctor> doctor = new List<Doctor>();
            doctor = _doctorDal.GetAll();
            
            return View(doctor);
        }

        public IActionResult Details(int id) 
        {
            Doctor doctor = new Doctor();
            doctor = _doctorDal.GetDoctorWithSchedule(id);

            return View(doctor);
        }

        public IActionResult Appointment(int id)
        {
            Doctor doctor = new Doctor();
            doctor = _doctorDal.GetById(1);
            doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(1);
            doctor.Schedules = _scheduleDal.GetSchedulesByDoctorId(1); 

            return View(doctor);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
