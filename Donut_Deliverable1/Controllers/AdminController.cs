using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Donut_Deliverable1.Models;
using Microsoft.AspNetCore.Authorization;
using Donut_Deliverable1.ViewModels;
using System.Globalization;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Routing;
using System.Configuration;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Donut_Deliverable1.Controllers
{
    
    public class AdminController : Controller
    {
        private IStudentRepository repository;
        private AppDbContext context;

        private IAttendanceRepository attendancerepository;
        private  AttendanceDbContext attendancecontext;

        public AdminController(AppDbContext context, IStudentRepository repo, AttendanceDbContext attendancecontext, IAttendanceRepository attendancerepo)
        {
            this.context = context;
            repository = repo;

            this.attendancecontext = attendancecontext;
            attendancerepository = attendancerepo;
        }

        public ActionResult Login()
        {
            return View();
        }
  
        public ActionResult Dashboard()
        {
            if (User.IsInRole("admin"))
            {
                Debug.WriteLine("hi get");


                    var date = DateTime.Today;
                    var students = context.Students;
                    var attendances = context.AttendanceLog;
                    var todaysAttendance = from a in attendances
                                           join s in students on a.nNumber equals s.nNumber
                                           where a.presentDateTime.Date == date.Date
                                           select new dayAttendance
                                           {
                                               Id = s.Id,
                                               firstName = s.firstName,
                                               lastName = s.lastName,
                                               nNumber = s.nNumber,
                                               datePresentTime = a.presentDateTime.ToLocalTime(),
                                               checkIn = (DateTime)a.checkIn,
                                               checkOut = (DateTime)a.checkOut,
                                               isExcused = (bool)a.isExcused,
                                               isTardy = (bool)a.isTardy,
                                               isAbsent = (bool)a.isAbsent,
                                               note = a.note
                                           };
                    var AttendanceViewModel = new AttendanceViewModel
                    {
                        QueryDate = date.Date.ToString("yyyy-MM-dd"),
                        dayAttendances = todaysAttendance
                    };

                    return View(AttendanceViewModel);
                
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dashboard([FromForm] CalendarGetter date)
        {
            if (User.IsInRole("admin"))
            {
                Debug.WriteLine("hi post");
                if (date.queryDate.Date.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    Debug.WriteLine("hi post bad date");
                    var today = DateTime.Today;
                    var students = context.Students;
                    var attendances = context.AttendanceLog;
                    var todaysAttendance = from a in attendances
                                           join s in students on a.nNumber equals s.nNumber
                                           where a.presentDateTime.Date == today.Date
                                           select new dayAttendance
                                           {
                                               Id = s.Id,
                                               firstName = s.firstName,
                                               lastName = s.lastName,
                                               nNumber = s.nNumber,
                                               datePresentTime = a.presentDateTime,
                                               checkIn = (DateTime)a.checkIn,
                                               checkOut = (DateTime)a.checkOut,
                                               isExcused = (bool)a.isExcused,
                                               isTardy = (bool)a.isTardy,
                                               isAbsent = (bool)a.isAbsent,
                                               note = a.note
                                           };
                    var AttendanceViewModel = new AttendanceViewModel
                    {
                        QueryDate = today.Date.ToString("yyyy-MM-dd"),
                        dayAttendances = todaysAttendance
                    };

                    return View(AttendanceViewModel);
                }
                else
                {
                    Debug.WriteLine("hi post good date");
                    var students = context.Students;
                    var attendances = context.AttendanceLog;
                    var daysAttendance = from a in attendances
                                         join s in students on a.nNumber equals s.nNumber
                                         where a.presentDateTime.Date == date.queryDate.Date
                                         select new dayAttendance
                                         {
                                             Id = s.Id,
                                             firstName = s.firstName,
                                             lastName = s.lastName,
                                             nNumber = s.nNumber,
                                             datePresentTime = a.presentDateTime,
                                             checkIn = (DateTime)a.checkIn,
                                             checkOut = (DateTime)a.checkOut,
                                             isExcused = (bool)a.isExcused,
                                             isTardy = (bool)a.isTardy,
                                             isAbsent = (bool)a.isAbsent,
                                             note = a.note
                                         };
                    var AttendanceViewModel = new AttendanceViewModel
                    {
                        QueryDate = date.queryDate.Date.ToString("yyyy-MM-dd"),
                        dayAttendances = daysAttendance
                    };

                    return View(AttendanceViewModel);
                }
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
        [HttpPost]
        public ActionResult UpdateExcused([FromBody] DashUpdateBool update)
        {
            System.Diagnostics.Debug.WriteLine("In Excused!");
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);
            char newBool;
            if (update.updateBool == "True")
                newBool = '1';
            else
                newBool = '0';

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);
            string queryString = "UPDATE [dbo].AttendanceLog SET isExcused = '" + newBool + "' WHERE CAST(presentDateTime AS DATE) = '" + theDate + "' AND nNumber = '" + update.nNumber + "'; ";
            SqlCommand excusedSet = new SqlCommand(queryString, con);

            //opens the server connection
            con.Open();
            //runs the SQL command
            excusedSet.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("Excused Sent!");

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateTardy([FromBody] DashUpdateBool update)
        {
            System.Diagnostics.Debug.WriteLine("In Tardy!");
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);
            char newBool;
            if (update.updateBool == "True")
                newBool = '1';
            else
                newBool = '0';

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);
            string queryString = "UPDATE [dbo].AttendanceLog SET isTardy = '" + newBool + "' WHERE CAST(presentDateTime AS DATE) = '" + theDate + "' AND nNumber = '" + update.nNumber + "'; ";
            SqlCommand tardySet = new SqlCommand(queryString, con);

            //opens the server connection
            con.Open();
            //runs the SQL command
            tardySet.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("Tardy Sent!");

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateAbsent([FromBody] DashUpdateBool update)
        {
            System.Diagnostics.Debug.WriteLine("In Absent!");
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);
            char newBool;
            if (update.updateBool == "True")
                newBool = '1';
            else
                newBool = '0';

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);
            string queryString = "UPDATE [dbo].AttendanceLog SET isAbsent = '" + newBool + "' WHERE CAST(presentDateTime AS DATE) = '" + theDate + "' AND nNumber = '" + update.nNumber + "'; ";
            SqlCommand absentSet = new SqlCommand(queryString, con);

            //opens the server connection
            con.Open();
            //runs the SQL command
            absentSet.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("Absent Sent!");

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateNote([FromBody] DashUpdateNote update)
        {
            System.Diagnostics.Debug.WriteLine("In Note!");
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);
            string queryString = "UPDATE [dbo].AttendanceLog SET note = '" + update.updateNote + "' WHERE CAST(presentDateTime AS DATE) = '" + theDate + "' AND nNumber = '" + update.nNumber + "'; ";
            SqlCommand noteSet = new SqlCommand(queryString, con);

            //opens the server connection
            con.Open();
            //runs the SQL command
            noteSet.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("Note Sent!");

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateCheckIn([FromBody] DashUpdateTime update)
        {
            System.Diagnostics.Debug.WriteLine("In CheckIn!");
            System.Diagnostics.Debug.WriteLine(update.updateTime);
            System.Diagnostics.Debug.WriteLine(update.presentDateTime);
            var originalDate = update.presentDateTime.Split(" ");
            string newCheckInString = originalDate[0] + " " + update.updateTime;
            System.Diagnostics.Debug.WriteLine(newCheckInString);

            DateTime theDate = Convert.ToDateTime(update.presentDateTime);
            DateTime newCheckIn = Convert.ToDateTime(newCheckInString);
            newCheckIn = newCheckIn.ToUniversalTime();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);
            string queryString = "UPDATE [dbo].AttendanceLog SET checkIn = '" + newCheckIn + "' WHERE CAST(presentDateTime AS DATE) = '" + theDate + "' AND nNumber = '" + update.nNumber + "'; ";
            SqlCommand noteSet = new SqlCommand(queryString, con);

            //opens the server connection
            con.Open();
            //runs the SQL command
            noteSet.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("CheckIn Sent!");

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateCheckOut([FromBody] DashUpdateTime update)
        {
            System.Diagnostics.Debug.WriteLine("In CheckOut!");
            System.Diagnostics.Debug.WriteLine(update.updateTime);

            DateTime theDate = Convert.ToDateTime(update.presentDateTime);
            DateTime newCheckOut = Convert.ToDateTime(update.updateTime);
            newCheckOut = newCheckOut.ToUniversalTime();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);
            string queryString = "UPDATE [dbo].AttendanceLog SET checkOut = '" + newCheckOut + "' WHERE CAST(presentDateTime AS DATE) = '" + theDate + "' AND nNumber = '" + update.nNumber + "'; ";
            SqlCommand noteSet = new SqlCommand(queryString, con);

            //opens the server connection
            con.Open();
            //runs the SQL command
            noteSet.ExecuteNonQuery();
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine("CheckOut Sent!");

            return new EmptyResult();
        }

        public ActionResult Report()
        {
            if (User.IsInRole("admin"))
            {
                var date = DateTime.Today;
                var currentWeekDay = -(int)date.DayOfWeek;
                var startOfWeek = date.AddDays(currentWeekDay);

                var students = context.Students;
                var attendances = context.AttendanceLog;
                var weekReportList = from a in attendances
                                       join s in students on a.nNumber equals s.nNumber
                                       where (a.presentDateTime.Date >= startOfWeek.Date && a.presentDateTime.Date <= date.Date) && ((Boolean)a.isAbsent || (Boolean)a.isTardy)
                                       select new reportList
                                       {
                                           Id = s.Id,
                                           firstName = s.firstName,
                                           lastName = s.lastName,
                                           nNumber = s.nNumber,
                                           tardy = (bool)a.isTardy,
                                           absent = (bool)a.isAbsent
                                       };
                List<reportSummary> summaries = new List<reportSummary>();
                foreach (reportList report in weekReportList)
                {
                    if (summaries.Exists(x => x.Id == report.Id))
                    {
                        if (report.tardy == true)
                        {
                            var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                            found.tardies++;
                        }
                        if (report.absent == true)
                        {
                            var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                            found.absences++;
                        }
                    }
                    else
                    {
                        summaries.Add(new reportSummary() { Id = report.Id, firstName = report.firstName, lastName = report.lastName, nNumber = report.nNumber, tardies = 0, absences = 0 });
                        if (report.tardy == true)
                        {
                            var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                            found.tardies++;
                        }
                        if (report.absent == true)
                        {
                            var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                            found.absences++;
                        }
                    }
                }
                var ReportViewModel = new ReportViewModel
                {
                    BeginningDate = startOfWeek.ToString("yyyy-MM-dd"),
                    EndingDate = date.ToString("yyyy-MM-dd"),
                    reports = summaries
                };

                return View(ReportViewModel);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report([FromForm] DoubleCalendarGetter dates)
        {
            if (User.IsInRole("admin"))
            {
                if (dates.firstDate.Date.ToString("yyyy-MM-dd") == "0001-01-01" || dates.secondDate.Date.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    var date = DateTime.Today;
                    var currentWeekDay = -(int)date.DayOfWeek;
                    var startOfWeek = date.AddDays(currentWeekDay);

                    var students = context.Students;
                    var attendances = context.AttendanceLog;
                    var weekReportList = from a in attendances
                                         join s in students on a.nNumber equals s.nNumber
                                         where (a.presentDateTime.Date >= startOfWeek.Date && a.presentDateTime.Date <= date.Date) && ((Boolean)a.isAbsent || (Boolean)a.isTardy)
                                         select new reportList
                                         {
                                             Id = s.Id,
                                             firstName = s.firstName,
                                             lastName = s.lastName,
                                             nNumber = s.nNumber,
                                             tardy = (bool)a.isTardy,
                                             absent = (bool)a.isAbsent
                                         };
                    List<reportSummary> summaries = new List<reportSummary>();
                    foreach (reportList report in weekReportList)
                    {
                        if (summaries.Exists(x => x.Id == report.Id))
                        {
                            if (report.tardy == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.tardies++;
                            }
                            if (report.absent == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.absences++;
                            }
                        }
                        else
                        {
                            summaries.Add(new reportSummary() { Id = report.Id, firstName = report.firstName, lastName = report.lastName, nNumber = report.nNumber, tardies = 0, absences = 0 });
                            if (report.tardy == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.tardies++;
                            }
                            if (report.absent == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.absences++;
                            }
                        }
                    }
                    var ReportViewModel = new ReportViewModel
                    {
                        BeginningDate = startOfWeek.ToString("yyyy-MM-dd"),
                        EndingDate = date.ToString("yyyy-MM-dd"),
                        reports = summaries
                    };

                    return View(ReportViewModel);
                }
                else
                {
                    var students = context.Students;
                    var attendances = context.AttendanceLog;
                    var weekReportList = from a in attendances
                                         join s in students on a.nNumber equals s.nNumber
                                         where (a.presentDateTime.Date >= dates.firstDate.Date && a.presentDateTime.Date <= dates.secondDate.Date) && ((Boolean)a.isAbsent || (Boolean)a.isTardy)
                                         select new reportList
                                         {
                                             Id = s.Id,
                                             firstName = s.firstName,
                                             lastName = s.lastName,
                                             nNumber = s.nNumber,
                                             tardy = (bool)a.isTardy,
                                             absent = (bool)a.isAbsent
                                         };
                    List<reportSummary> summaries = new List<reportSummary>();
                    foreach (reportList report in weekReportList)
                    {
                        if (summaries.Exists(x => x.Id == report.Id))
                        {
                            if (report.tardy == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.tardies++;
                            }
                            if (report.absent == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.absences++;
                            }
                        }
                        else
                        {
                            summaries.Add(new reportSummary() { Id = report.Id, firstName = report.firstName, lastName = report.lastName, nNumber = report.nNumber, tardies = 0, absences = 0 });
                            if (report.tardy == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.tardies++;
                            }
                            if (report.absent == true)
                            {
                                var found = summaries.FirstOrDefault(s => s.Id == report.Id);
                                found.absences++;
                            }
                        }
                    }
                    var ReportViewModel = new ReportViewModel
                    {
                        BeginningDate = dates.firstDate.Date.ToString("yyyy-MM-dd"),
                        EndingDate = dates.secondDate.Date.ToString("yyyy-MM-dd"),
                        reports = summaries
                    };

                    return View(ReportViewModel);
                }
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
    }
}
