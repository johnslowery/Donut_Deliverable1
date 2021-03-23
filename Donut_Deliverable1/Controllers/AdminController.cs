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
                var students = context.Students;
                var attendances = context.AttendanceLog;
                var todaysAttendance = from a in attendances
                                       join s in students on a.nNumber equals s.nNumber
                                       where a.presentDateTime.Date == DateTime.Today.Date
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
                    QueryDate = DateTime.Today.ToString("yyyy-MM-dd"),
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

                if (date.queryDate.Date.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    var students = context.Students;
                    var attendances = context.AttendanceLog;
                    var todaysAttendance = from a in attendances
                                           join s in students on a.nNumber equals s.nNumber
                                           where a.presentDateTime.Date == DateTime.Today.Date
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
                        QueryDate = DateTime.Today.ToString("yyyy-MM-dd"),
                        dayAttendances = todaysAttendance
                    };

                    return View(AttendanceViewModel);
                }
                else
                {
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
        public EmptyResult UpdateExcused([FromBody] DashUpdateBool update)
        {
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);

            var updateAttendance = context.AttendanceLog.Where(x => x.nNumber == update.nNumber && x.presentDateTime.Date == theDate.Date).First();
            bool newBool;
            if (update.updateBool == "True")
                newBool = true;
            else
                newBool = false;
            updateAttendance.isExcused = newBool;
            context.SaveChanges();

            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult UpdateTardy([FromBody] DashUpdateBool update)
        {
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);

            var updateAttendance = context.AttendanceLog.Where(x => x.nNumber == update.nNumber && x.presentDateTime.Date == theDate.Date).First();
            bool newBool;
            if (update.updateBool == "True")
                newBool = true;
            else
                newBool = false;
            updateAttendance.isTardy = newBool;
            context.SaveChanges();

            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult UpdateAbsent([FromBody] DashUpdateBool update)
        {
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);

            var updateAttendance = context.AttendanceLog.Where(x => x.nNumber == update.nNumber && x.presentDateTime.Date == theDate.Date).First();
            bool newBool;
            if (update.updateBool == "True")
                newBool = true;
            else
                newBool = false;
            updateAttendance.isAbsent = newBool;
            context.SaveChanges();

            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult UpdateNote([FromBody] DashUpdateNote update)
        {
            DateTime theDate = Convert.ToDateTime(update.presentDateTime);

            var updateAttendance = context.AttendanceLog.Where(x => x.nNumber == update.nNumber && x.presentDateTime.Date == theDate.Date).First();

            updateAttendance.note = update.updateNote;
            context.SaveChanges();

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
        public ActionResult Report([FromForm] CalendarGetter BeginningDate, CalendarGetter EndingDate)
        {
            if (User.IsInRole("admin"))
            {
                Debug.WriteLine(BeginningDate.queryDate);
                Debug.WriteLine(EndingDate.queryDate);
                if (BeginningDate.queryDate.Date.ToString("yyyy-MM-dd") == "0001-01-01" || EndingDate.queryDate.Date.ToString("yyyy-MM-dd") == "0001-01-01")
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
                                         where (a.presentDateTime.Date >= BeginningDate.queryDate.Date && a.presentDateTime.Date <= EndingDate.queryDate.Date) && ((Boolean)a.isAbsent || (Boolean)a.isTardy)
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
                        BeginningDate = BeginningDate.queryDate.Date.ToString("yyyy-MM-dd"),
                        EndingDate = EndingDate.queryDate.Date.ToString("yyyy-MM-dd"),
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
