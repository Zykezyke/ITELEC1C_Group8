using ITELEC1C_Group8.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITELEC1C_Group8.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _dbData;
        private readonly UserManager<User> _userManager;

        public PortfolioController(AppDbContext dbData, UserManager<User> userManager)
        {
            _dbData = dbData;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            // Retrieve all users from the database
            List<User> allUsers = await _dbData.Users.ToListAsync();

            List<Doctor> doctorUsers = new List<Doctor>();

            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "Doctor"))
                {
                    doctorUsers.Add((Doctor)user);
                }
            }

            // You can pass both lists to the view or use ViewData
            ViewData["DoctorUsers"] = doctorUsers;

            return View();
        }
    }
}
