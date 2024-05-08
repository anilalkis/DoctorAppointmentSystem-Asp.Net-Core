using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {
            if(ModelState.IsValid) 
            {
                AppUser user = new AppUser {
                    
                    FullName = p.FullName,
                    UserName = p.Username,
                    Email = p.Email,
                };

                var result = await _userManager.CreateAsync(user,p.Password);

                if (result.Succeeded) 
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                        await Console.Out.WriteLineAsync(item.Description);
                    }
                }
            }

            return View(p);
        }
    }
}
