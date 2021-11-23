using System;
using System.Collections.Generic;

namespace UCP1_PAW_087_C.Models
{
    public partial class Attendance
    {
        public int IdAttendance { get; set; }
        public int IdUser { get; set; }
        public DateTime? CheckInDatetime { get; set; }
        public DateTime? CheckOutDatetime { get; set; }

        public virtual Users IdUserNavigation { get; set; }
    }
}
