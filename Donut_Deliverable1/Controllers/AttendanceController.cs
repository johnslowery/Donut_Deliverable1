using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Donut_Deliverable1.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Donut_Deliverable1.Controllers
{
    public class AttendanceController : Controller
    {
        private IStudentRepository repository;
        private readonly AppDbContext context;

        private IAttendanceRepository attendancerepository;
        private readonly AttendanceDbContext attendancecontext;


        public AttendanceController(AppDbContext context, IStudentRepository repo, AttendanceDbContext attendancecontext, IAttendanceRepository attendancerepo)
        {
            this.context = context;
            repository = repo;

            this.attendancecontext = attendancecontext;
            attendancerepository = attendancerepo;
        }
        public IActionResult CheckIn()
        {
            if (User.IsInRole("admin") || User.IsInRole("attendance"))
            {
                return View();
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }
        }



        [HttpPost]
        public IActionResult Verification(string nNumber)
        {
            //Makes sure that the n-Number matches specified format
            Regex pattern = new Regex(@"^[Nn][0-9]{8}$");

            if (!pattern.IsMatch(nNumber))
            {
                return Content("Make sure you enter a valid n-Number");
            }


            //Checks student list for matching N-number
            var CurrStudent = repository.GetStudent(nNumber);


            return View(CurrStudent);


        }


        public IActionResult Success(Student currentStudent)

            //now to figure out how to get the stuff to show
        {
            //need to figure out for to get the student to insert into the attendance log and check if they have already checked in prior

            /*            var GetDate = DateTime.Now;

                        Attendance.(GetDate, currentStudent.nNumber);*/



            var currentTime = DateTime.Now;

            //finds attendance log for current student on the current date
            var currentAttend = attendancerepository.GetAttendance(currentTime, currentStudent.nNumber);




            return View(currentStudent);
        }

    }
}
