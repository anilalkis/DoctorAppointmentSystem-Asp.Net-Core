using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Index(MailRequest mailRequest)
        {


            return View();
        }
    }
}
