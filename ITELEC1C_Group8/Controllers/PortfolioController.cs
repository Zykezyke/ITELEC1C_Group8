﻿using Microsoft.AspNetCore.Mvc;

namespace ITELEC1C_Group8.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
