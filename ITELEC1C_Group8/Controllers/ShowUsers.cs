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
            if (!User.IsInRole("Admin"))
            {
                Console.WriteLine("User does not have the Admin role.");

                return RedirectToAction("Index", "Home");
            }

            List<User> users = await _dbData.Users.ToListAsync();
            return View(users);
        }
    }
}
