using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Donut_Deliverable1.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verification(string nNumber)
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
