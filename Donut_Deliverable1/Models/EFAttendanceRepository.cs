using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.Models
{
    public class EFAttendanceRepository : IAttendanceRepository
    {
        private AttendanceDbContext attendancecontext;
        private List<Attendance> _attendanceList;
        public EFAttendanceRepository(AttendanceDbContext ctx)
        {
            attendancecontext = ctx;
        }
        public IEnumerable<Attendance> AttendanceLog
        {
            get
            {
                return attendancecontext.AttendanceLog;
            }
        }
        public Attendance GetAttendance(DateTime presentDateTime, string nNumber)
        {
            _attendanceList = attendancecontext.AttendanceLog.ToList();
            return _attendanceList.FirstOrDefault(e => e.presentDateTime.Date == presentDateTime.Date && e.nNumber == nNumber);
        }

        public Attendance GetAttendance(DateTime presentDateTime)
        {
            _attendanceList = attendancecontext.AttendanceLog.ToList();
            return _attendanceList.FirstOrDefault(e => e.presentDateTime.Date == presentDateTime.Date);
        }

 
        public Attendance GetAttendance(string nNumber)
        {
            _attendanceList = attendancecontext.AttendanceLog.ToList();
            return _attendanceList.FirstOrDefault(e => e.nNumber == nNumber);
        }

    }
}
