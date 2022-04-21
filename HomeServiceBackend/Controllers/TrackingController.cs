using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeServiceBackend.Models;


namespace HomeServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private ApplicationContext db;
        public TrackingController(ApplicationContext context)
        {
            db = context;
        }

        //Маршруты и координаты

        [HttpGet("getRoutesByEmployeeId/{id}")]
        public LogicalRoute getRoutesByEmployeeId(int id)
        {
            List<Employee_to_plan> Tasks_to_emp = new List<Employee_to_plan>();
            List<Routes> usroutes = new List<Routes>();
            List<List<Coordinates>> route = new List<List<Coordinates>>();
            var polyroute = new List<PolyRoute>();
            var polyline = new List<List<float>>();
            var employee_to_plan = db.employee_to_plan.ToList();
            foreach (var fromid_emtoplan in employee_to_plan)
            {
                if (fromid_emtoplan.employeeid == id)
                {
                    Tasks_to_emp.Add(fromid_emtoplan);
                }
            }
            var routes = db.routes.ToList();
            foreach (var each in Tasks_to_emp)
            {
                foreach (var fromid_routes in routes)
                {
                    if (fromid_routes.epid == each.id)
                    {
                        usroutes.Add(fromid_routes);
                    }
                }
            }
            var coordinates = db.coordinates.ToList();
            for (int i = 0; i < usroutes.Count; i++)
            {
                route.Add(new List<Coordinates>());
                polyroute.Add(new PolyRoute());
            }
            int index = 0;
            foreach (var each in usroutes)
            {
                foreach (var fromid_cdnts in coordinates)
                {
                    if (fromid_cdnts.employeeid == id
                        && fromid_cdnts.time >= each.stime
                        && fromid_cdnts.time <= each.etime)
                    {
                        route[index].Add(fromid_cdnts);
                        var flist = new List<float>();
                        flist.Add(fromid_cdnts.lat);
                        flist.Add(fromid_cdnts.lng);
                        polyline.Add(flist);
                        polyroute[index].employeeId = fromid_cdnts.employeeid;
                        polyroute[index].fio = db.employees.SingleOrDefault(x => x.id == fromid_cdnts.employeeid).fio;
                    }
                }
                polyroute[index].polyline = polyline;
                polyline = new List<List<float>>();
                index++;
            }
            var model = new LogicalRoute
            {
                routes = route,
                polyroutes = polyroute
            };
            return model;

        }

        [HttpGet("getRoutesByPlanId/{id}")]
        public LogicalRoute getRoutesByPlanId(int id)
        {
            List<Employee_to_plan> Tasks_to_emp = new List<Employee_to_plan>();
            List<Routes> usroutes = new List<Routes>();
            List<List<Coordinates>> route = new List<List<Coordinates>>();
            var polyroute = new List<PolyRoute>();
            var polyline = new List<List<float>>();
            var employee_to_plan = db.employee_to_plan.ToList();
            foreach (var fromid_emtoplan in employee_to_plan)
            {
                if (fromid_emtoplan.planid == id)
                {
                    Tasks_to_emp.Add(fromid_emtoplan);
                }
            }
            var routes = db.routes.ToList();
            foreach (var each in Tasks_to_emp)
            {
                foreach (var fromid_routes in routes)
                {
                    if (fromid_routes.epid == each.id)
                    {
                        usroutes.Add(fromid_routes);
                    }
                }
            }
            var coordinates = db.coordinates.ToList();
            for (int i = 0; i < usroutes.Count; i++)
            {
                route.Add(new List<Coordinates>());
                polyroute.Add(new PolyRoute());
            }
            int index = 0;
            foreach (var each in usroutes)
            {
                foreach (var fromid_cdnts in coordinates)
                {
                    if (fromid_cdnts.employeeid == db.employee_to_plan.SingleOrDefault(x => x.id == each.epid).employeeid
                        && fromid_cdnts.time >= each.stime
                        && fromid_cdnts.time <= each.etime)
                    {
                        route[index].Add(fromid_cdnts);
                        var flist = new List<float>();
                        flist.Add(fromid_cdnts.lat);
                        flist.Add(fromid_cdnts.lng);
                        polyline.Add(flist);
                        polyroute[index].employeeId = fromid_cdnts.employeeid;
                        polyroute[index].fio = db.employees.SingleOrDefault(x => x.id == fromid_cdnts.employeeid).fio;
                    }
                }
                polyroute[index].polyline = polyline;
                polyline = new List<List<float>>();
                index++;
            }
            var model = new LogicalRoute
            {
                routes = route,
                polyroutes = polyroute
            };
            return model;

        }

        [HttpGet("getLastLocationAll")]
        public IEnumerable<Coordinates> getLastLocationAll()
        {
            List<Coordinates> LastCoordinates = new List<Coordinates>();
            List<DateTime> maxDates = new List<DateTime>();
            List<int> all_emp_id = new List<int>();
            foreach (var coordinate in db.coordinates.ToList())
            {
                if (!all_emp_id.Contains(coordinate.employeeid))
                    all_emp_id.Add(coordinate.employeeid);
            }
            int index = 0;
            foreach (var emp_id in all_emp_id)
            {
                DateTime maxDate = DateTime.MinValue;
                foreach (var coordinate in db.coordinates.ToList())
                {
                    if (coordinate.employeeid == emp_id)
                    {
                        if (coordinate.time > maxDate)
                            maxDate = coordinate.time;
                    }
                }
                maxDates.Add(maxDate);
                LastCoordinates.Add(db.coordinates.SingleOrDefault(x => x.employeeid == emp_id
                && x.time == maxDates[index]));
                index++;
            }
            return LastCoordinates;
        }


        [HttpGet("getLastLocationById/{id}")]
        public Coordinates getLastLocationById(int id)
        {
            DateTime maxDate = DateTime.MinValue;
            foreach (var coordinate in db.coordinates.ToList())
            {
                if (coordinate.employeeid == id)
                {
                    if (coordinate.time > maxDate)
                        maxDate = coordinate.time;
                }
            }
            return db.coordinates.SingleOrDefault(x => x.employeeid == id && x.time == maxDate);
        }
    }
}
