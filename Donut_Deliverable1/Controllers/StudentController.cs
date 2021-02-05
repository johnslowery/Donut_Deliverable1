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
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            return View(students);
        }
        public IActionResult StudentSearch()
        {
            var students = repository.Students.ToList();
            return View(students);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            context.Add(student);
            context.SaveChanges();
            return RedirectToAction("StudentSearch");
        }

        public ActionResult Edit()
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            context.Update(student);
            context.SaveChanges();
            return RedirectToAction("StudentSearch");
        }

    }
}
