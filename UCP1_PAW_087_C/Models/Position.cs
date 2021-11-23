using System;
using System.Collections.Generic;

namespace UCP1_PAW_087_C.Models
{
    public partial class Position
    {
        public Position()
        {
            Users = new HashSet<Users>();
        }

        public int IdPosition { get; set; }
        public string PositionName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
