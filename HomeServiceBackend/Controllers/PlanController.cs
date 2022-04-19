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
    public class PlanController : ControllerBase
    {
        private ApplicationContext db;
        public PlanController(ApplicationContext context)
        {
            db = context;
        }
        
        // План
        [HttpPost("addPlan")]
        public void addPlan([FromBody] forAddPlan planandemps)
        {
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
        }

        [HttpPut("updatePlan/{id}")]
        public void updatePlan(int id, [FromBody] forAddPlan planandemps)
        {
            planandemps.plan.id = id;
            foreach (var f in db.employee_to_plan)
            {
                if (f.planid  == id)
                {
                    db.employee_to_plan.Remove(f);
                }
            }
            if (planandemps.empids.Count > 0)
            {
                foreach (int empid in planandemps.empids)
                {
                    var emt = new Employee_to_plan(planandemps.plan.id, empid);
                    db.employee_to_plan.Add(emt);
                }
            }
            db.plans.Update(planandemps.plan);
            db.SaveChanges();
        }

        [HttpDelete("deletePlan/{id}")]
        public void deletePlan(int id)
        {
            var item = db.plans.FirstOrDefault(x => x.id == id);
            foreach (var f in db.employee_to_plan)
            {
                if (f.planid == id)
                {
                    db.employee_to_plan.Remove(f);
                }
            }
            if (item != null)
            {
                //db.plans.Remove(item);
                db.plans.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }
        
        [HttpGet("getAllPlans")]
        public IEnumerable<PlanInfo> getAllPlans()
        {
            List<PlanInfo> plans_with_emps = new List<PlanInfo>();
            var plans = db.plans.Where(x => x.deleted == false).ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in plans)
            {
                PlanInfo planinfo = new PlanInfo();
                planinfo.plan = item;
                planinfo.employees = new List<dynamic>();
                planinfo.property = db.propertys.SingleOrDefault(x => x.id == item.propertyid);
                planinfo.client = db.clients.SingleOrDefault(x => x.id == planinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (item.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        planinfo.employees.Add(empx);
                    }
                }
                plans_with_emps.Add(planinfo);
            }
            return plans_with_emps;
        }

        [HttpGet("getPlansByDate/{date}")]
        public IEnumerable<PlanInfo> getPlansByDate(DateTime date)
        {
            List<Plans> qplans = new List<Plans>();
            var plans = db.plans.Where(x => x.deleted == false).ToList();
            foreach (var item in plans)
            {
                if (item.date.Date == date.Date)
                {
                    qplans.Add(item);
                }
            }
            List<PlanInfo> plans_with_emps = new List<PlanInfo>();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in qplans)
            {
                PlanInfo planinfo = new PlanInfo();
                planinfo.plan = item;
                planinfo.employees = new List<dynamic>();
                planinfo.property = db.propertys.SingleOrDefault(x => x.id == item.propertyid);
                planinfo.client = db.clients.SingleOrDefault(x => x.id == planinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (item.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        planinfo.employees.Add(empx);
                    }
                }
                plans_with_emps.Add(planinfo);
            }
            return plans_with_emps;
        }

        [HttpGet("getPlanByEmployeeId/{id}")]
        public IEnumerable<PlanInfo> getPlanByEmployeeId(int id)
        {
            List<PlanInfo> plans_with_emps = new List<PlanInfo>();
            var plans = db.plans.Where(x => x.deleted == false).ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in plans)
            {
                PlanInfo planinfo = new PlanInfo();
                planinfo.plan = item;
                planinfo.employees = new List<dynamic>();
                planinfo.property = db.propertys.SingleOrDefault(x => x.id == item.propertyid);
                planinfo.client = db.clients.SingleOrDefault(x => x.id == planinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (item.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        planinfo.employees.Add(empx);
                    }
                }
                if (planinfo.employees.Exists(x => x.Employee.id == id))
                {
                    plans_with_emps.Add(planinfo);
                }
            }
            return plans_with_emps;
        }

        [HttpGet("getPlansByDateAndEmployeeId/{date}&&{id}")]
        public IEnumerable<PlanInfo> getPlansByDateAndEmployeeId(DateTime date, int id)
        {
            List<Plans> qplans = new List<Plans>();
            var plans = db.plans.Where(x => x.deleted == false).ToList();
            foreach (var item in plans)
            {
                if (item.date.Date == date.Date)
                {
                    qplans.Add(item);
                }
            }
            List<PlanInfo> plans_with_emps = new List<PlanInfo>();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in qplans)
            {
                PlanInfo planinfo = new PlanInfo();
                planinfo.plan = item;
                planinfo.employees = new List<dynamic>();
                planinfo.property = db.propertys.SingleOrDefault(x => x.id == item.propertyid);
                planinfo.client = db.clients.SingleOrDefault(x => x.id == planinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (item.id == epid.planid)
                    {
                        var empx =
                        new
                        {
                            Employee = db.employees.SingleOrDefault(x => x.id == epid.employeeid),
                            Employees_functions = db.employees_functions.SingleOrDefault
                            (x => x.id == db.employees.SingleOrDefault(x => x.id == epid.employeeid).function_id).name
                        };
                        planinfo.employees.Add(empx);
                    }
                }
                if (planinfo.employees.Exists(x => x.Employee.id == id))
                {
                    plans_with_emps.Add(planinfo);
                }
            }
            return plans_with_emps;
        }
    }
}
