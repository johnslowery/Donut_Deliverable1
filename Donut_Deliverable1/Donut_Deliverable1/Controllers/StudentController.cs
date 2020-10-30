using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donut_Deliverable1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Donut_Deliverable1.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository repository;
        private readonly AppDbContext context;

        public StudentController(AppDbContext context, IStudentRepository repo)
        {
            this.context = context;
            repository = repo;
        }
        public IActionResult StudentView()
        {
            return View();
        }
        public IActionResult StudentSearch()
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
