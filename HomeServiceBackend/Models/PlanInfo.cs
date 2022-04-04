using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class PlanInfo
    {
        public Plans plan { get; set; }
        public List<Employees> employees { get; set; }
        public Propertys property { get; set; }
        public Clients client { get; set; }
    }

    public class forAddPlan
    {
        public Plans plan { get; set; }
        public List<int> empids { get; set; }
    }
}
