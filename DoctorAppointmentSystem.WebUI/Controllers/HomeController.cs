using DoctorAppointmentSystem.WebUI.Models;
using DoctorAppointmentSystem.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DoctorAppointmentSystem.Data.Abstract;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPatientDal _patientDal;
        private IAppointmentDal _appointmentDal;

        public HomeController(ILogger<HomeController> logger, IPatientDal patientDal, IAppointmentDal appointmentDal)
        {
            _logger = logger;
            _patientDal = patientDal;
            _appointmentDal = appointmentDal;
        }

        public IActionResult Index()
        {

            PatientWithAppointmentViewModel patient = new PatientWithAppointmentViewModel();
            patient.Patient = _patientDal.GetById(1);
            patient.Appointments = _appointmentDal.GetAppointmentsWithPatientId(1);

            return View(patient);
        }

        public IActionResult Privacy()
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
