using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
            //Makes sure that the nNumber matches specified format
            Regex pattern = new Regex(@"^[Nn][0-9]{8}$");

            if (!pattern.IsMatch(nNumber)) {
                return Content("Make sure you enter a valid n-Number");
            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }


/*
        [HttpPost]
        public IActionResult CheckIn(string nNumber, DateTime GetDate)
        {
            //Still needs validation that user exists and to add to a database
            GetDate = DateTime.Now;
            return Content($"Successful Check-in, {nNumber} \nCheck in Time:{GetDate}");
        }*/
    }
}
