using DoctorAppointmentSystem.WebUI.Models;
using DoctorAppointmentSystem.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DoctorAppointmentSystem.Data.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DoctorAppointmentSystem.WebUI.EmailServices;
using Microsoft.AspNetCore.Identity.UI.Services;

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
        private IDayOffDal _dayOffDal;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailSender;


        public PatientController(ILogger<PatientController> logger, IPatientDal patientDal, IAppointmentDal appointmentDal, IDoctorDal doctorDal, IScheduleDal scheduleDal, UserManager<AppUser> userManager, IEmailService emailSender, IDayOffDal dayOffDal)
        {
            _logger = logger;
            _patientDal = patientDal;
            _appointmentDal = appointmentDal;
            _doctorDal = doctorDal;
            _scheduleDal = scheduleDal;
            _userManager = userManager;
            _emailSender = emailSender;
            _dayOffDal = dayOffDal;
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

            patient.Appointments = _appointmentDal.GetAppointmentsWithPatientId(patient.PatientId)
                .OrderByDescending(appointment => appointment.DateTime)
                .Take(5)
                .ToList(); 

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
            model.daysOff = _dayOffDal.GetDaysByDoctorId(id);
           
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Appointment(int patientId, int doctorId, string date)
        {
            var patient = _patientDal.GetById(patientId);
            var doctor = _doctorDal.GetById(doctorId);

            Appointment appointment = new Appointment();

            appointment.DateTime = DateTime.Parse(date);
            appointment.PatientId = patientId;
            appointment.DoctorId = doctorId;
            appointment.IsApproved = State.Waiting;
            appointment.Note = " ";

            _emailSender.Execute("anilalkis86@gmail.com", "Appointment Information", $"Dear {patient.FullName},\n\nYour appointment request has been received. Detailed information is added below.\n\n Appointment Date: {appointment.DateTime.ToShortTimeString()} \n\n Appointment Time: {appointment.DateTime.ToLongDateString()}\n\nDoctor Name: {doctor.FullName}\n\nAppointment Status: Waiting\n\nWe wish you a good day.\n\nMedisen");
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
