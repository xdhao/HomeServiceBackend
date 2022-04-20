using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class LogicalRoute
    {
        public List<List<Coordinates>> routes { get; set; }
        public List<List<List<float>>> polylines { get; set; }
    }
}
