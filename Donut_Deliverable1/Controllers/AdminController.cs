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
        private IStudentRepository repository;
        private readonly AppDbContext context;

        public AdminController(AppDbContext context, IStudentRepository repo)
        {
            this.context = context;
            repository = repo;
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            var students = context.Students;
            return View(students);
        }

        public ActionResult Report()
        {
            var students = context.Students;
            return View(students);
        }
    }
}
