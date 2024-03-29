using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Donut_Deliverable1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public String nNumber { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String scholarship { get; set; }
        public String birthDate { get; set; }

        public Boolean archived { get; set; }
        public byte[] studentImage { get; set; }
    }
}
