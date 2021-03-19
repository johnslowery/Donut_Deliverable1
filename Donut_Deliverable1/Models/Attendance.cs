﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.Models
{
    public class Attendance
    {

        public string nNumber { get; set; }

        public DateTime presentDateTime { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }

        public Boolean isExcused { get; set; }
        public Boolean isTardy { get; set; }
        public Boolean isAbsent { get; set; }

        public string note { get; set; }


    }


}
