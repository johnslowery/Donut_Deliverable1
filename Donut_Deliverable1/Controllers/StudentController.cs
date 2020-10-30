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
        public async Task<IActionResult> StudentSearch(string searchString, string searchType)
        {
            var students = from v in context.Students select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                if(searchType == "nnumber")
                {
                    students = students.Where(s => s.nNumber.Contains(searchString));
                }else if (searchType == "fname")
                {
                    students = students.Where(s => s.firstName.Contains(searchString));
                }else if (searchType == "lname")
                {
                    students = students.Where(s => s.lastName.Contains(searchString));
                }else if (searchType == "scholarship")
                {
                    students = students.Where(s => s.scholarship.Contains(searchString));
                }/*else if (searchType == "year")
                {
                    students = students.Where(s => s.currentYear.Contains(searchString));
                }else if (searchType == "age")
                {
                    students = students.Where(s => s.age.Contains(searchString));
                }*/
            }
            return View(await students.ToListAsync());
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
