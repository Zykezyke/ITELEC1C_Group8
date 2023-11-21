using ITELEC1C_Group8.Data;
using ITELEC1C_Group8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITELEC1C_Group8.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbData;
        private readonly UserManager<User> _userManager;

        public ContactController(AppDbContext dbData, UserManager<User> userManager)
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
        public IActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContact(Contact newContact)
        {
            newContact.SetUserInfo(_userManager, User);
            if (!ModelState.IsValid)
                return View();

            _dbData.Contacts.Add(newContact);
            _dbData.SaveChanges();
            this.TempData["messages"] = "Message Sent!";
            return RedirectToAction("Index");
        }



    }
}