using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult CheckIn(string nNumber, DateTime GetDate)
        {
            //Still needs validation that user exists and to add to a database
            GetDate = DateTime.Now;
            return Content($"Successful Check-in, {nNumber} \nCheck in Time:{GetDate}");
        }
    }
}
