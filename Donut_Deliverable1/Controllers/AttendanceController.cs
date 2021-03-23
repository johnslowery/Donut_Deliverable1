﻿
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
using System.Data;
using System.Data.SqlClient;


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


        public ActionResult Verification()
        {
            return View(new Attendance());
        }


        [HttpPost]
        public ActionResult Verification(Attend objUserModel)
        {

            System.Diagnostics.Debug.WriteLine("Verify");
            //Goes to verification if n-Number is valid
            if (ModelState.IsValid)
            {
                var CurrStudent = repository.GetStudent(objUserModel.nNumber);
                return View(CurrStudent);
            }

            //retuns to check in page if n-Number is not valid
            return View("CheckIn");
        }



        public IActionResult Success()
        {

            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var currentStudent = repository.GetStudent(studentId);
            System.Diagnostics.Debug.WriteLine("in success");
            System.Diagnostics.Debug.WriteLine(currentStudent.nNumber);
            //Date used to determine when a student is considered late
            DateTime lateTime = DateTime.Parse("2012/12/12 16:00:00.000");


            SqlConnection con = new SqlConnection("Server=tcp:arcdb.database.windows.net,1433;Initial Catalog=arcDB;Persist Security Info=False;User ID=serveradmin;Password=#FakePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //SQL Command to add students check in time
            SqlCommand checkinset = new SqlCommand(@"UPDATE [dbo].[AttendanceLog] 
                SET checkIn = GETDATE() 
                WHERE CAST(presentDateTime AS DATE) = CAST(GETDATE() AS DATE) 
                AND checkIn IS NULL 
                AND nNumber = '" + currentStudent.nNumber + "'; ", con);
            //SQL Command to add students checout
            SqlCommand checkoutset = new SqlCommand(@"UPDATE [dbo].[AttendanceLog] 
                SET checkOut = GETDATE() 
                WHERE CAST(presentDateTime AS DATE) = CAST(GETDATE() AS DATE) 
                AND checkOut IS NULL 
                AND checkIn IS NOT NULL
                AND nNumber = '" + currentStudent.nNumber + "'; ", con);
            // SQL command to mark a student as late 
            SqlCommand markLate = new SqlCommand(@"UPDATE [dbo].AttendanceLog 
                SET isTardy = '1' 
                WHERE CAST(presentDateTime AS TIME) >= CAST('" + lateTime + "' AS TIME)" +
            " AND nNumber = '" + currentStudent.nNumber + "' " +
            "AND CAST(presentDateTime AS DATE) = CAST(GETDATE() AS DATE)" +
            "AND checkOut IS NULL;", con);

            //opens the server connection
            con.Open();
            System.Diagnostics.Debug.WriteLine("Connection open");
            //runs the SQL commands
            checkoutset.ExecuteNonQuery();
            checkinset.ExecuteNonQuery();
            markLate.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("Done");

            return View(currentStudent);
        }

    }
}