using ITELEC1C_Group8.Data;
using ITELEC1C_Group8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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



        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        
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
            
            _dbData.Appointments.Add(newAppointment);
            _dbData.SaveChanges();
            return RedirectToAction("Index");
        }

        
        
    }
}
