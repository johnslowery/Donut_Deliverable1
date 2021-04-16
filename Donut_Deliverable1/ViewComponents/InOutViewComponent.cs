using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donut_Deliverable1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using Microsoft.Azure;
using System.Configuration;
using System.Web;

namespace Donut_Deliverable1.ViewComponents
{
    public class InOutViewComponent : ViewComponent
    {
        private readonly IStudentRepository studentRepository;

        private IAttendanceRepository attendancerepository;
        private readonly AttendanceDbContext attendancecontext;

        public InOutViewComponent(IStudentRepository studentRepository, IAttendanceRepository attendancerepo)
        {
            this.studentRepository = studentRepository;
            attendancerepository = attendancerepo;
        }

        public IViewComponentResult Invoke(string number)
        {
            System.Diagnostics.Debug.WriteLine("before break");
            System.Diagnostics.Debug.WriteLine(number);
            System.Diagnostics.Debug.WriteLine("after numn");


            var currentStudent = studentRepository.GetStudent(number);

            System.Diagnostics.Debug.WriteLine(currentStudent.nNumber);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentData"].ConnectionString);

            SqlCommand inorout = new SqlCommand(@"select checkIn from [dbo].[AttendanceLog] 
                            WHERE CAST(presentDateTime AS DATE) = CAST(GETDATE() AS DATE) 
                            AND nNumber = '" + currentStudent.nNumber + "'; ", con);


            con.Open();
            System.Diagnostics.Debug.WriteLine("Connection open");
            //runs the SQL commands
            var hi = inorout.ExecuteScalar();
            string getusername = inorout.ExecuteScalar() as string;
            System.Diagnostics.Debug.WriteLine(getusername);
            //closes the server connection
            con.Close();

            System.Diagnostics.Debug.WriteLine(hi);
            string status = "Out";
            if (hi is DBNull)
            {
                status = "In";
            }

            return View((object)status);
        }
    }
}
