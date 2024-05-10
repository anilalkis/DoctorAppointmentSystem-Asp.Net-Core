using DoctorAppointmentSystem.WebUI.Models;
using DoctorAppointmentSystem.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DoctorAppointmentSystem.Data.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private IPatientDal _patientDal;
        private IAppointmentDal _appointmentDal;
        private IDoctorDal _doctorDal;
        private IScheduleDal _scheduleDal;
        private readonly UserManager<AppUser> _userManager;


        public PatientController(ILogger<PatientController> logger, IPatientDal patientDal, IAppointmentDal appointmentDal, IDoctorDal doctorDal, IScheduleDal scheduleDal, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _patientDal = patientDal;
            _appointmentDal = appointmentDal;
            _doctorDal = doctorDal;
            _scheduleDal = scheduleDal;
            _userManager = userManager; 
        }

        public IActionResult Index()
        {
            
            string? UserEmail = _userManager.GetUserName(User);
            if(UserEmail == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var patient = _patientDal.GetByUserEmail(UserEmail);

            if (patient == null)
            {
                return NotFound();
            }

            patient.Appointments = _appointmentDal.GetAppointmentsWithPatientId(patient.PatientId);

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
            if (id == 0)
            {
                return RedirectToAction("DoctorList");
            }

            string? UserName = _userManager.GetUserName(User);


            if (UserName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            PatientAppointmentViewModel model = new PatientAppointmentViewModel();
            model.Patient = _patientDal.GetByUserEmail(UserName);
            model.Doctor = _doctorDal.GetById(id);
            model.Doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(id);
            model.Doctor.Schedules = _scheduleDal.GetSchedulesByDoctorId(id);
           
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Appointment(int patientId, int doctorID, string date)
        {

            Appointment appointment = new Appointment();

            appointment.DateTime = DateTime.Parse(date);
            appointment.PatientId = patientId;
            appointment.DoctorId = doctorID;
            appointment.IsApproved = false;

            _appointmentDal.Create(appointment);

            return RedirectToAction("Index");
        }

        public IActionResult MyAppointments()
        {
            string? UserName = _userManager.GetUserName(User);

            if (UserName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var patient = _patientDal.GetByUserEmail(UserName);
            patient.Appointments = _appointmentDal.GetAppointmentsWithPatientId(patient.PatientId);

            return View(patient);
        }

        [HttpPost]
        public IActionResult MyAppointments(int appointmentId)
        {
            Appointment appointment = _appointmentDal.GetAppointmentById(appointmentId);
            _appointmentDal.Delete(appointment);

            return RedirectToAction("Index");
        }

        public IActionResult Settings() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
