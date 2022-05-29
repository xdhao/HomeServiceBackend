using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class Mobile
    {
        public string login { get; set; }
        public string password { get; set; }
    }

    public class RouteFromMob
    {
        public int planid { get; set; }
        public int empid { get; set; }
        public DateTime stime { get; set; }
    }

    public class RepFromMob
    {
        public int planid { get; set; }
        public int empid { get; set; }
        public string com { get; set; }
    }

}
