using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public LoginController(SignInManager<AppUser> sigInManager, UserManager<AppUser> userManager)
		{
            _signInManager = sigInManager;
            _userManager = userManager;

        }

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserSignInViewModel user)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, true);
				if (result.Succeeded)
				{
                    var p = await _userManager.FindByNameAsync(user.Username);
                    HttpContext.Session.SetString("Email", p.Email);
                    if (await _userManager.IsInRoleAsync( p,"Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (await _userManager.IsInRoleAsync(p, "Doctor"))
                    {
                        return RedirectToAction("Index", "Doctor");
                    }
                    else if (await _userManager.IsInRoleAsync(p, "Patient"))
                    {
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        return RedirectToAction("AccessDenied", "Home");
                    }
                }
				else
				{
                    return RedirectToAction("Index", "Login");
				}
			}

			return View();
		}
	}
}
