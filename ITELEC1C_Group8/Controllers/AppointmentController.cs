using ITELEC1C_Group8.Data;
using ITELEC1C_Group8.Enums;
using ITELEC1C_Group8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ITELEC1C_Group8.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _dbData;
        private readonly UserManager<User> _userManager;

        public AppointmentController(AppDbContext dbData, UserManager<User> userManager)
        {
            _dbData = dbData;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetDoctorsByBranch(Branch selectedBranch)
        {
            var doctors = _dbData.Doctors.Where(d => d.Branch == selectedBranch).ToList();
            var doctorList = doctors.Select(d => new SelectListItem
            {
                Text = d.FirstName + " " + d.LastName,
                Value = d.UserName
            }).ToList();
            return Json(doctorList);
        }


        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult AddApp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddApp(Appointment newAppointment)
        {   
            newAppointment.SetUserInfo(_userManager, User);
            if (!ModelState.IsValid)
                return View();


            var doctorValue = ModelState["SelectedDoctor"]?.AttemptedValue;
            _dbData.Appointments.Add(newAppointment);
            _dbData.SaveChanges();
            this.TempData["messages"] = "Appointment Booked";
            return RedirectToAction("AddApp", "Appointment");
        }

        
        
    }
}
