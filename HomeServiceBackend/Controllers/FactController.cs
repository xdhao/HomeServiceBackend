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
    public class FactController : ControllerBase
    {
        private ApplicationContext db;
        public FactController(ApplicationContext context)
        {
            db = context;
        }

        //Факт

        /// <summary>
        /// Gives all facts data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllFacts")]
        public IEnumerable<FactInfo> getAllFacts()
        {
            TrackingController tracking = new TrackingController(db);
            List<FactInfo> facts_fullinfo = new List<FactInfo>();
            var facts = db.facts.Where(x => x.deleted == false).ToList();
            var reports = db.reports.Where(x => x.deleted == false).ToList();
            var routes = db.routes.Where(x => x.deleted == false).ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in facts)
            {
                FactInfo factinfo = new FactInfo();
                factinfo.fact = item;
                factinfo.fact = new
                {
                    Fact = item,
                    Workname = db.works.SingleOrDefault(x => x.id == item.workid).name
                };
                factinfo.plan = new
                {
                    Plan = db.plans.SingleOrDefault(x => x.id == item.planid),
                    Workname = db.works.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).workid).name
                };
                factinfo.employees = new List<dynamic>();
                factinfo.reports = new List<RepInfo>();
                factinfo.property = db.propertys.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).propertyid);
                factinfo.client = db.clients.SingleOrDefault(x => x.id == factinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (factinfo.plan.Plan.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        factinfo.employees.Add(empx);
                        foreach (var rp in reports)
                        {
                            if (rp.epid == epid.id)
                            {
                                var rpep = new RepInfo();
                                rpep.rep = rp;
                                rpep.empid = epid.employeeid;
                                var fio = db.employees.SingleOrDefault(x => x.id == epid.employeeid).surname + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).name + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).patronymic;
                                rpep.fio = fio;
                                factinfo.reports.Add(rpep);
                            }
                        }
                        factinfo.routes = tracking.getRoutesByPlanId(epid.planid);
                    }
                }
                facts_fullinfo.Add(factinfo);
            }
            return facts_fullinfo;
        }

        /// <summary>
        /// Gives facts data by ID of employee.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getFactByEmployeeId/{id}")]
        public IEnumerable<FactInfo> getFactByEmployeeId(int id)
        {
            TrackingController tracking = new TrackingController(db);
            List<FactInfo> facts_fullinfo = new List<FactInfo>();
            var facts = db.facts.Where(x => x.deleted == false).ToList();
            var reports = db.reports.Where(x => x.deleted == false).ToList();
            var routes = db.routes.Where(x => x.deleted == false).ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in facts)
            {
                FactInfo factinfo = new FactInfo();
                factinfo.fact = new
                {
                    Fact = item,
                    Workname = db.works.SingleOrDefault(x => x.id == item.workid).name
                };
                factinfo.plan = new
                {
                    Plan = db.plans.SingleOrDefault(x => x.id == item.planid),
                    Workname = db.works.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).workid).name
                };
                factinfo.employees = new List<dynamic>();
                factinfo.reports = new List<RepInfo>();
                factinfo.property = db.propertys.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).propertyid);
                factinfo.client = db.clients.SingleOrDefault(x => x.id == factinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (factinfo.plan.Plan.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        factinfo.employees.Add(empx);
                        foreach (var rp in reports)
                        {
                            if (rp.epid == epid.id)
                            {
                                var rpep = new RepInfo();
                                rpep.rep = rp;
                                rpep.empid = epid.employeeid;
                                var fio = db.employees.SingleOrDefault(x => x.id == epid.employeeid).surname + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).name + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).patronymic;
                                rpep.fio = fio;
                                factinfo.reports.Add(rpep);
                            }
                        }
                        factinfo.routes = tracking.getRoutesByPlanId(epid.planid);
                    }
                }
                if (factinfo.employees.Exists(x => x.Employee.id == id))
                {
                    facts_fullinfo.Add(factinfo);
                }
            }
            return facts_fullinfo;
        }

        /// <summary>
        /// Gives facts data by date.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getFactsByDate/{date}")]
        public IEnumerable<FactInfo> getFactsByDate(DateTime date)
        {
            TrackingController tracking = new TrackingController(db);
            List<Facts> qfacts = new List<Facts>();
            var facts = db.facts.Where(x => x.deleted == false).ToList();
            foreach (var item in facts)
            {
                var dplan = db.plans.SingleOrDefault(x => x.id == item.planid);
                if (dplan.date.Date == date.Date)
                {
                    qfacts.Add(item);
                }
            }
            List<FactInfo> facts_fullinfo = new List<FactInfo>();
            var reports = db.reports.Where(x => x.deleted == false).ToList();
            var routes = db.routes.Where(x => x.deleted == false).ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in qfacts)
            {
                FactInfo factinfo = new FactInfo();
                factinfo.fact = new
                {
                    Fact = item,
                    Workname = db.works.SingleOrDefault(x => x.id == item.workid).name
                };
                factinfo.plan = new
                {
                    Plan = db.plans.SingleOrDefault(x => x.id == item.planid),
                    Workname = db.works.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).workid).name
                };
                factinfo.employees = new List<dynamic>();
                factinfo.reports = new List<RepInfo>();
                factinfo.property = db.propertys.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).propertyid);
                factinfo.client = db.clients.SingleOrDefault(x => x.id == factinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (factinfo.plan.Plan.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        factinfo.employees.Add(empx);
                        foreach (var rp in reports)
                        {
                            if (rp.epid == epid.id)
                            {
                                var rpep = new RepInfo();
                                rpep.rep = rp;
                                rpep.empid = epid.employeeid;
                                var fio = db.employees.SingleOrDefault(x => x.id == epid.employeeid).surname + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).name + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).patronymic;
                                rpep.fio = fio;
                                factinfo.reports.Add(rpep);
                            }
                        }
                        factinfo.routes = tracking.getRoutesByPlanId(epid.planid);
                    }
                }
                facts_fullinfo.Add(factinfo);
            }
            return facts_fullinfo;
        }

        /// <summary>
        /// Gives facts data by ID of employee and date.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getFactsByDateAndEmployeeId/{date}&&{id}")]
        public IEnumerable<FactInfo> getFactsByDateAndEmployeeId(DateTime date, int id)
        {
            TrackingController tracking = new TrackingController(db);
            List<Facts> qfacts = new List<Facts>();
            var facts = db.facts.Where(x => x.deleted == false).ToList();
            foreach (var item in facts)
            {
                var dplan = db.plans.SingleOrDefault(x => x.id == item.planid);
                if (dplan.date.Date == date.Date)
                {
                    qfacts.Add(item);
                }
            }
            List<FactInfo> facts_fullinfo = new List<FactInfo>();
            var reports = db.reports.Where(x => x.deleted == false).ToList();
            var routes = db.routes.Where(x => x.deleted == false).ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in qfacts)
            {
                FactInfo factinfo = new FactInfo();
                factinfo.fact = new
                {
                    Fact = item,
                    Workname = db.works.SingleOrDefault(x => x.id == item.workid).name
                };
                factinfo.plan = new
                {
                    Plan = db.plans.SingleOrDefault(x => x.id == item.planid),
                    Workname = db.works.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).workid).name
                };
                factinfo.employees = new List<dynamic>();
                factinfo.reports = new List<RepInfo>();
                factinfo.property = db.propertys.SingleOrDefault(x => x.id == db.plans.SingleOrDefault(x => x.id == item.planid).propertyid);
                factinfo.client = db.clients.SingleOrDefault(x => x.id == factinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (factinfo.plan.Plan.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        factinfo.employees.Add(empx);
                        foreach (var rp in reports)
                        {
                            if (rp.epid == epid.id)
                            {
                                var rpep = new RepInfo();
                                rpep.rep = rp;
                                rpep.empid = epid.employeeid;
                                var fio = db.employees.SingleOrDefault(x => x.id == epid.employeeid).surname + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).name + ' ' +
                                    db.employees.SingleOrDefault(x => x.id == epid.employeeid).patronymic;
                                rpep.fio = fio;
                                factinfo.reports.Add(rpep);
                            }
                        }
                        factinfo.routes = tracking.getRoutesByPlanId(epid.planid);
                    }
                }
                if (factinfo.employees.Exists(x => x.Employee.id == id))
                {
                    facts_fullinfo.Add(factinfo);
                }
            }
            return facts_fullinfo;
        }

        /// <summary>
        /// Create new fact.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addFact")]
        public void addFact([FromBody] forAddPlan planandemps)
        {
            planandemps.plan.deleted = true;
            db.plans.Add(planandemps.plan);
            db.SaveChanges();
            if (planandemps.empids.Count > 0)
            {
                foreach (int empid in planandemps.empids)
                {
                    var emt = new Employee_to_plan(planandemps.plan.id, empid);
                    db.employee_to_plan.Add(emt);
                    db.SaveChanges();
                }
            }
            var newfact = new Facts
            {
                planid = planandemps.plan.id,
                workid = planandemps.plan.workid,
                propertyid = planandemps.plan.propertyid,
                date = planandemps.plan.date,
                count = planandemps.plan.count,
                number_of_people = planandemps.plan.number_of_people,
                hours = planandemps.plan.hours
            };
            db.facts.Add(newfact);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete fact by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteFact/{id}")]
        public void deleteFact(int id)
        {
            var item = db.facts.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.facts.Remove(item);
                db.facts.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }
    }
}
