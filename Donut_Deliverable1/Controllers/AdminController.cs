using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Donut_Deliverable1.Models;

namespace Donut_Deliverable1.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
    }
}
