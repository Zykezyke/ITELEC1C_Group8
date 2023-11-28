using Microsoft.AspNetCore.Mvc;
using ITELEC1C_Group8.Data;
using ITELEC1C_Group8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ITELEC1C_Group8.Controllers
{
    public class ShowUsersController : Controller
    {
        private readonly AppDbContext _dbData;
        private readonly UserManager<User> _userManager;

        public ShowUsersController(AppDbContext dbData, UserManager<User> userManager)
        {
            _dbData = dbData;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            // Check if the current user has the Admin role
            if (!User.IsInRole("Admin"))
            {
                // If not an admin, you might want to redirect or show an error
                Console.WriteLine("User does not have the Admin role.");
                return RedirectToAction("Index", "Home");
            }

            // Retrieve all users from the database
            List<User> allUsers = await _dbData.Users.ToListAsync();

            // Separate users into two lists based on role
            List<User> regularUsers = new List<User>();
            List<User> adminUsers = new List<User>();

            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    adminUsers.Add(user);
                }
                else
                {
                    regularUsers.Add(user);
                }
            }

            // You can pass both lists to the view or use ViewData
            ViewData["RegularUsers"] = regularUsers;
            ViewData["AdminUsers"] = adminUsers;

            return View();
        }
    }
}
