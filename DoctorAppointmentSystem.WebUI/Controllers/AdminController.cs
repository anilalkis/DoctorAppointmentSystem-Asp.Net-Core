using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
