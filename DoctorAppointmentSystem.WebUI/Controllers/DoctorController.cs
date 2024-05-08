using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
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
            Doctor doctor = new Doctor();
            doctor = _doctorDal.GetByUserName(_userManager.GetUserName(User));
            doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(doctor.DoctorId);
            return View(doctor);
        }

        public IActionResult AppointmentList() 
        {
            Doctor doctor = new Doctor();
            doctor = _doctorDal.GetByUserName(_userManager.GetUserName(User));
            doctor.Appointments = _appointmentDal.GetAppointmentsWithDoctorId(doctor.DoctorId);

            return View(doctor.Appointments);
        }

        public IActionResult Schedule()
        {
            Doctor doctor = new Doctor();
            doctor = _doctorDal.GetByUserName(_userManager.GetUserName(User));
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
    }
}
