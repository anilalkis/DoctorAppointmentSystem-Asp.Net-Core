using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager; 
        private IPatientDal _patientDal;
        private IDoctorDal _doctorDal;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, IPatientDal patientDal, IDoctorDal doctorDal, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _patientDal = patientDal;
            _doctorDal = doctorDal;
            _userManager = userManager;
        }

        public IActionResult RoleList()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string newRole)
        {
            if(newRole == null) 
            {
                return NotFound();
            }

            await _roleManager.CreateAsync(new AppRole() { Name = newRole });

            return RedirectToAction("RoleList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        { 
            if(id == 0) 
            {
                return NotFound();
            }

             var role = await _roleManager.FindByIdAsync(id.ToString());

            if(role == null) 
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("RoleList");
        }
    }
}
