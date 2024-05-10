using DoctorAppointmentSystem.Data.Abstract;
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
        private readonly IPatientDal _patientDal;

        public RegisterController(UserManager<AppUser> userManager, IPatientDal patientDal)
        {
            _userManager = userManager;
            _patientDal = patientDal;
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
                    UserName = p.Email,
                    Email = p.Email,
                };

                var result = await _userManager.CreateAsync(user,p.Password);

                if (result.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(user, "Patient");
                    _patientDal.Create(new Patient()
                    {
                        FullName=p.FullName,
                        Email=p.Email,
                        Phone=p.Phone
                    });
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
