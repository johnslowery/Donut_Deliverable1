using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Donut_Deliverable1.Models;
using Microsoft.AspNetCore.Authorization;

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
            if (User.IsInRole("admin"))
            {
                var students = context.Students;
                return View(students);
            } 
            else if (User.IsInRole("attendance"))
            {
                return RedirectToAction("CheckIn", "Attendance");
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }
        }

        public ActionResult Report()
        {
            if (User.IsInRole("admin"))
            {
                var students = context.Students;
                return View(students);
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }
        }
    }
}
