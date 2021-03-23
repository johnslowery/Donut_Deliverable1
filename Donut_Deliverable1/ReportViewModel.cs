using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.ViewModels
{
    public class ReportViewModel
    {
        public String BeginningDate { get; set; }
        public String EndingDate { get; set; }
        public IEnumerable<reportSummary> reports { get; set; }
    }

    public class reportSummary
    {
        public int Id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String nNumber { get; set; }
        public int tardies { get; set; }
        public int absences { get; set; }
    }

    public class reportList
    {
        public int Id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String nNumber { get; set; }
        public bool tardy { get; set; }
        public bool absent { get; set; }
    }
}
