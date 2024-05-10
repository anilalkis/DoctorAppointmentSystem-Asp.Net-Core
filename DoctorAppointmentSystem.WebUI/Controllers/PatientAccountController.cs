using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IPatientDal _patientDal;

        public PatientAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPatientDal patientDal)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _patientDal = patientDal;
        }

        [HttpGet]
        public IActionResult AccountSettings()
        {
            string? UserName = _userManager.GetUserName(User);
            if (UserName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var patient = _patientDal.GetByUserEmail(UserName);

            if (patient == null)
            {
                return NotFound();
            }

            var model = new Patient() 
            { 
                FullName=patient.FullName,
                Age=patient.Age,
                Gender=patient.Gender,
                Email=patient.Email,
                Phone=patient.Phone,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AccountSettings(Patient model)
        {
            if (ModelState.IsValid) 
            {
                string? UserEmail = _userManager.GetUserName(User);
                if (UserEmail == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var patient = _patientDal.GetByUserEmail(UserEmail);

                if (patient == null) 
                {
                    return NotFound();
                }

                var user = await _userManager.GetUserAsync(User);

                if(user == null) 
                {
                    return NotFound();
                }

                patient.FullName = model.FullName;
                patient.Gender = model.Gender;
                patient.Email = model.Email;
                patient.Phone = model.Phone;
                patient.Age = model.Age;

                _patientDal.Update(patient);
                await _userManager.SetEmailAsync(user, model.Email);
                await _userManager.SetUserNameAsync(user, model.Email);
                await _signInManager.RefreshSignInAsync(user);

                
            }

            return RedirectToAction("Index","Patient");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AcoountSettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if ((model.OldPassword != null) && (model.NewPassword != null))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details() 
        {
            string? UserName = _userManager.GetUserName(User);
            if (UserName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var patient = _patientDal.GetByUserEmail(UserName);

            if (patient == null)
            {
                return NotFound();
            }

            var model = new Patient()
            {
                FullName = patient.FullName,
                Age = patient.Age,
                Gender = patient.Gender,
                Email = patient.Email,
                Phone = patient.Phone,
            };

            return View(model);
        }

        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            string? UserName = _userManager.GetUserName(User);
            if (UserName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var patient = _patientDal.GetByUserEmail(UserName);
            if (patient == null) 
            {
                return NotFound();
            }

            _patientDal.Delete(patient);
            await _userManager.DeleteAsync(user);


            return RedirectToAction("Logout","Login");
        }
    }
}
