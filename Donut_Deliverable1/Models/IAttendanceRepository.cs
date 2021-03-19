using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.Models
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> AttendanceLog { get; }
        Attendance GetAttendance(DateTime presentDateTime, string nNumber);
        Attendance GetAttendance(DateTime presentDateTime);
        Attendance GetAttendance(string nNumber);
    }
}
