using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class FactInfo
    {
        public dynamic fact { get; set; }
        public dynamic plan { get; set; }
        public List<dynamic> employees { get; set; }
        public Propertys property { get; set; }
        public Clients client { get; set; }
        public LogicalRoute routes { get; set; }
        public List<RepInfo> reports { get; set; }
    }

    public class RepInfo
    {
        public Reports rep { get; set; }
        public int empid { get; set; }
        public string fio { get; set; }

    }
}
