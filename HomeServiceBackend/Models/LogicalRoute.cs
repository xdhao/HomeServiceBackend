using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class LogicalRoute
    {
        public List<List<Coordinates>> routes { get; set; }
        public List<PolyRoute> polyroutes { get; set; }
    }

    public class PolyRoute
    {
        public int employeeId { get; set; }
        public string fio { get; set; }
        public List<List<float>> polyline { get; set; }
    }
}
