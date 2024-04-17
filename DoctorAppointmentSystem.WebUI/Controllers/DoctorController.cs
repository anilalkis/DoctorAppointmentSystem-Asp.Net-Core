using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class DoctorController : Controller
    {
        private IPatientDal _patientDal;
        private IAppointmentDal _appointmentDal;
        private IDoctorDal _doctorDal;
        private IScheduleDal _scheduleDal;

        public DoctorController(IPatientDal patientDal, IAppointmentDal appointmentDal, IDoctorDal doctorDal, IScheduleDal scheduleDal)
        {
            _patientDal = patientDal;
            _appointmentDal = appointmentDal;
            _doctorDal = doctorDal;
            _scheduleDal = scheduleDal;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AppointmentList() 
        {
            List<Appointment> appointments = new List<Appointment>();
            appointments = _appointmentDal.GetAppointmentsWithDoctorId(1);

            return View(appointments);
        }

        public IActionResult Schedule()
        {
            List<Schedule> schedules = new List<Schedule>();
            schedules = _scheduleDal.GetSchedulesByDoctorId(1);

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

            }
            else
            {
                appointment.IsApproved = false;
            } 


            _appointmentDal.Update(appointment);

            return RedirectToAction("AppointmentList");
        }
    }
}
