using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Donut_Deliverable1.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;

namespace Donut_Deliverable1
{
    public class AttendanceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {


                SqlConnection con = new SqlConnection("Connection string here");

                var nNumber = Convert.ToString(value);
                SqlCommand studentExist = new SqlCommand(@"Select * from [dbo].[Students] WHERE nNumber = '" + nNumber + "';", con);
                int item;
                con.Open();
                //runs the SQL commands
                object o = studentExist.ExecuteScalar();
                item = o == null ? 0 : (int)o;
                //closes the server connection
                con.Close();

                if (item == 0)
                {
                    return new ValidationResult("No student found with that N-Number");
                }
            }
            return ValidationResult.Success;
        }
    }
}
