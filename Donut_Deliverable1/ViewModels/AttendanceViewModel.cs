using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donut_Deliverable1.Models;

namespace Donut_Deliverable1.ViewModels
{
    public class AttendanceViewModel
    {
        public String QueryDate { get; set; }
        public IEnumerable<dayAttendance> dayAttendances { get; set; }
        
    }

    public class dayAttendance
    {
        public int Id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String nNumber { get; set; }
        public DateTime datePresentTime { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public bool isExcused { get; set; }
        public bool isTardy { get; set; }
        public bool isAbsent { get; set; }
        public String note { get; set; }
    }
}
