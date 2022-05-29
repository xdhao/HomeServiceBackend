using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeServiceBackend.Models;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private ApplicationContext db;
        public MobileController(ApplicationContext context)
        {
            db = context;
        }

        // для авторизации в мобильном приложении
        [HttpPost("getUser")]
        public dynamic getUser(Mobile user)
        {
            var emp = db.employees.SingleOrDefault(x => x.email == user.login && x.password == user.password);
            var res = new
            {
                Employee = emp,
                func_name = db.employees_functions.SingleOrDefault(x => x.id == emp.function_id).name
            };
            return res;
        }

        // запись полученных координат в бд
        [HttpPost("takegps")]
        public void takegps(Coordinates coordinate)
        {
            db.coordinates.Add(coordinate);
            db.SaveChanges();
        }

        // сохранить время начала маршрута работника при выполнении конкртеной задачи
        [HttpGet("startRoute/{planid}&&{empid}&&{time}")]
        public void startRoute(int planid, int empid, DateTime time)
        {
            var ep = db.employee_to_plan.SingleOrDefault(x => x.employeeid == empid && x.planid == planid).id;
            if (db.routes.Any(x => x.epid == ep) == false)
            {
                var route = new Routes();
                route.epid = ep;
                route.stime = time;
                db.routes.Add(route);
                db.SaveChanges();
            }
            else
            {
                db.routes.SingleOrDefault(x => x.epid == ep).stime = time;
                db.SaveChanges();
            }
        }

        // сохранить время окончания маршрута
        [HttpGet("endRoute/{planid}&&{empid}&&{time}")]
        public void endRoute(int planid, int empid, DateTime time)
        {
            var ep = db.employee_to_plan.SingleOrDefault(x => x.employeeid == empid && x.planid == planid).id;
            db.routes.SingleOrDefault(x => x.epid == ep).etime = time;
            db.SaveChanges();
        }

        // запись полученного от моб. приложения отчета о выполненной работе
        [HttpPost("genRep")]
        public void genRep(RepFromMob repm)
        {
            var rep = new Reports();
            rep.comment = repm.com;
            rep.epid = db.employee_to_plan.SingleOrDefault(x => x.employeeid == repm.empid && x.planid == repm.planid).id;
            db.reports.Add(rep);
            db.SaveChanges();
        }

    }
}
