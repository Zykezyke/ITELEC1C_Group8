using ITELEC1C_Group8.Data;
using ITELEC1C_Group8.Models;
using ITELEC1C_Group8.ViewModels;
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



        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var userContacts = _dbData.Contacts.Where(c => c.UserId == userId).ToList();

            var viewModel = new ContactViewModel
            {
                Contacts = userContacts,
                NewContact = new Contact()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "User")]
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

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int contactId)
        {
            var contactToDelete = _dbData.Contacts.Find(contactId);

            if (contactToDelete != null)
            {
                _dbData.Contacts.Remove(contactToDelete);
                _dbData.SaveChanges();
                TempData["messages"] = "Contact deleted successfully.";
            }
            else
            {
                TempData["messages"] = "Contact not found.";
            }

            // Get the current user's roles
            var roles = _userManager.GetRolesAsync(_userManager.GetUserAsync(User).Result).Result;

            // Check if the user has the "Admin" role
            if (roles.Contains("Admin"))
            {

                return RedirectToAction("ShowContact");
            }
            else
            {
  
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowContact()
        {
            var contacts = _dbData.Contacts.ToList(); // Retrieve all contacts from the database
            return View(contacts);
        }



    }
}