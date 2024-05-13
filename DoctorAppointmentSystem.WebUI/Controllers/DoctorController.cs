using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    [Authorize(Roles = "Doctor,Admin")]
    public class DoctorController : Controller
    {
        private IPatientDal _patientDal;
        private IAppointmentDal _appointmentDal;
        private IDoctorDal _doctorDal;
        private IScheduleDal _scheduleDal;
        private readonly UserManager<AppUser> _userManager;


        public DoctorController(IPatientDal patientDal, IAppointmentDal appointmentDal, IDoctorDal doctorDal, IScheduleDal scheduleDal, UserManager<AppUser> userManager)
        {
            _patientDal = patientDal;
            _appointmentDal = appointmentDal;
            _doctorDal = doctorDal;
            _scheduleDal = scheduleDal;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            string? UserEmail = _userManager.GetUserName(User);

            if (UserEmail == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var doctor = _doctorDal.GetByUserEmail(UserEmail);
            doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(doctor.DoctorId);
            
            return View(doctor);
        }

        public IActionResult AppointmentList() 
        {
            string? UserEmail = _userManager.GetUserName(User);

            if (UserEmail == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var doctor = _doctorDal.GetByUserEmail(UserEmail);
            doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(doctor.DoctorId)
                .Where(x => x.DateTime >= DateTime.Now).ToList();

            return View(doctor.Appointments);
        }
        public IActionResult PastAppointmentList()
        {
            string? UserEmail = _userManager.GetUserName(User);

            if (UserEmail == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var doctor = _doctorDal.GetByUserEmail(UserEmail);
            doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(doctor.DoctorId)
                .Where(x => x.DateTime < DateTime.Now).ToList();

            return View(doctor.Appointments);
        }

        public IActionResult Schedule()
        {
            string? UserEmail = _userManager.GetUserName(User);

            if (UserEmail == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var doctor = _doctorDal.GetByUserEmail(UserEmail);
            List<Schedule> schedules = new List<Schedule>();
            schedules = _scheduleDal.GetSchedulesByDoctorId(doctor.DoctorId);

            return View(schedules);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int AppointmentId)
        {
            var appointment = _appointmentDal.GetById(AppointmentId);

            if(appointment == null)
            {
                return NotFound();
            }
            
            if(appointment.IsApproved == false)
            {
                appointment.IsApproved = true;
                TempData["ConfirmMessage"] = "appointment confirmed.";

            }
            else
            {
                appointment.IsApproved = false;
                TempData["CancelMessage"] = "appointment has been cancelled.";
            } 


            _appointmentDal.Update(appointment);

            return RedirectToAction("AppointmentList");
        }

        public IActionResult PatientProfile(int id) 
        {
            if (id == 0)
            {
                return NotFound();
            }
            var patient = _patientDal.GetById(id);
            patient.Appointments = _appointmentDal.GetAppointmentsWithPatientId(id);

            return View(patient);
        }

        [HttpGet]
        public IActionResult AppointmentDetails(int id)
        {
            if(id == 0) 
            {
                return NotFound();
            }
            var app = _appointmentDal.GetAppointmentById(id);

            return View(app);
        }

        [HttpPost]
        public IActionResult AppointmentDetails(string note,int id)
        {
            if(id==0)
            {
                return NotFound();
            }

            var app = _appointmentDal.GetAppointmentById(id);
            var newapp = new Appointment()
            {
                AppointmentId = app.AppointmentId,
                DateTime = app.DateTime,
                IsApproved = app.IsApproved,
                PatientId = app.PatientId,
                DoctorId = app.DoctorId,
                Note = note,
            };
            _appointmentDal.Update(newapp);
            newapp = _appointmentDal.GetAppointmentById(id);
            return View(newapp);
        }
    }
}
