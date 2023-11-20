using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITELEC1C_Group8.Controllers
{
    public class ContactController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}

