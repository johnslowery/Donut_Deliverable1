using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Donut_Deliverable1.Models;
using System.Security.Claims;

namespace Donut_Deliverable1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string role;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var claims = User.Claims.ToList();

            foreach (Claim x in claims)
            {
                if (x.Type == "Role")
                {
                    role = x.Value;
                    Console.WriteLine(x.Value);
                }
            }

            if (role == "Admin")
            {
                ViewData["Role"] = "Admin";
                Console.WriteLine(ViewBag.Role);
                return RedirectToAction("Privacy");
            }
            else if (role == "Attendance")
            {
                ViewBag.Role = "Attendance";
                return RedirectToAction("CheckIn", "Attendance");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
