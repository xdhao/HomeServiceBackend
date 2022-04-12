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

        [HttpGet("getAllFacts")]
        public IEnumerable<FactInfo> getAllFacts()
        {
            TrackingController tracking = new TrackingController(db);
            List<FactInfo> facts_fullinfo = new List<FactInfo>();
            var facts = db.facts.ToList();
            var reports = db.reports.ToList();
            var routes = db.routes.ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in facts)
            {
                FactInfo factinfo = new FactInfo();
                factinfo.fact = item;
                factinfo.plan = db.plans.SingleOrDefault(x => x.id == item.planid);
                factinfo.employees = new List<Employees>();
                factinfo.reports = new List<RepInfo>();
                factinfo.property = db.propertys.SingleOrDefault(x => x.id == factinfo.plan.propertyid);
                factinfo.client = db.clients.SingleOrDefault(x => x.id == factinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (factinfo.plan.id == epid.planid)
                    {
                        factinfo.employees.Add(db.employees.SingleOrDefault(x => x.id == epid.employeeid));
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


        [HttpGet("getFactsByDate/{date}")]
        public IEnumerable<FactInfo> getFactsByDate(DateTime date)
        {
            TrackingController tracking = new TrackingController(db);
            List<Facts> qfacts = new List<Facts>();
            var facts = db.facts.ToList();
            foreach (var item in facts)
            {
                var dplan = db.plans.SingleOrDefault(x => x.id == item.planid);
                if (dplan.date.Date == date.Date)
                {
                    qfacts.Add(item);
                }
            }
            List<FactInfo> facts_fullinfo = new List<FactInfo>();
            var reports = db.reports.ToList();
            var routes = db.routes.ToList();
            var emptoplan = db.employee_to_plan.ToList();
            foreach (var item in qfacts)
            {
                FactInfo factinfo = new FactInfo();
                factinfo.fact = item;
                factinfo.plan = db.plans.SingleOrDefault(x => x.id == item.planid);
                factinfo.employees = new List<Employees>();
                factinfo.reports = new List<RepInfo>();
                factinfo.property = db.propertys.SingleOrDefault(x => x.id == factinfo.plan.propertyid);
                factinfo.client = db.clients.SingleOrDefault(x => x.id == factinfo.property.client_id);
                foreach (var epid in emptoplan)
                {
                    if (factinfo.plan.id == epid.planid)
                    {
                        factinfo.employees.Add(db.employees.SingleOrDefault(x => x.id == epid.employeeid));
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

        [HttpDelete("deleteFact/{id}")]
        public void deleteFact(int id)
        {
            var item = db.facts.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.facts.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
