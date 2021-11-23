using System;
using System.Collections.Generic;

namespace UCP1_PAW_087_C.Models
{
    public partial class Users
    {
        public Users()
        {
            Attendance = new HashSet<Attendance>();
        }

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? IdRole { get; set; }
        public int? IdPosition { get; set; }

        public virtual Position IdPositionNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
    }
}
