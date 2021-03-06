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
    public class AnaliticController : ControllerBase
    {
        private ApplicationContext db;
        public AnaliticController(ApplicationContext context)
        {
            db = context;
        }


        /*
         Возвращает кол-во выполнений работ в заданный период. Выводятся лишь те работы, которые имеют хотя бы одно выполнение за указанный промежуток.
         */
        /// <summary>
        /// Gives the number of executions of work by time period.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getWorksPerfomanceRatio/{startdate}&&{enddate}")]
        public dynamic getWorksPerfomanceRatio(DateTime startdate, DateTime enddate)
        {
            var allWorks = db.works.ToList();
            var facts = db.facts.ToList();
            var res = new List<dynamic>();
            int usecount = 0;
            foreach (var work in allWorks)
            {
                foreach (var fact in facts)
                {
                    if (fact.workid == work.id && (fact.date > startdate && fact.date < enddate))
                    {
                        usecount++;
                    }
                }
                if (usecount > 0)
                {
                    var re = new
                    {
                        Workname = work.name,
                        Count = usecount
                    };
                    res.Add(re);
                    usecount = 0;
                }
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range";
        }

        /*
         * Возвращает кол-во выполненных работ в выходные и рабочие дни в указанный промежуток.
         */
        /// <summary>
        /// Gives the number of jobs on workweek and weekends.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getWorkingDays/{startdate}&&{enddate}")]
        public dynamic getWorkingDays(DateTime startdate, DateTime enddate)
        {
            int weekends = 0;
            int workweek = 0;
            var facts = db.facts.ToList();
            foreach (var fact in facts)
            {
                if (fact.date > startdate && fact.date < enddate)
                {
                    if (fact.date.DayOfWeek == DayOfWeek.Sunday || fact.date.DayOfWeek == DayOfWeek.Saturday)
                        weekends++;
                    else
                        workweek++;
                }
            }
            if (weekends > 0 || workweek > 0)
            {
                var res = new
                {
                    Weekends = weekends,
                    Workweek = workweek
                };
                return res;
            }
            else 
                return "No data available for current year/date range";
        }


        /*
         * Возвращает количество затраченных на выполнение каждой работы часов за каждый месяц указанного года. 
         * Вывод значений производится лишь для тех работ, которые выполнялись хотя бы раз.
         */
        /// <summary>
        /// Gives the number of hours spent on work  for each month.
        /// </summary>
        /// <returns></returns>
        [HttpGet("countAllWorksHours/{year}")]
        public dynamic countAllWorksHours(int year)
        {
            List<Count> res = new List<Count>();
            var works = db.works.ToList();
            var curwork = new Works();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach(var work in works)
            {
                foreach (var fact in facts)
                {
                    if (fact.workid == work.id && fact.date.Year == year)
                    {
                        curwork = work;
                        ch.work = new
                        {
                            workid = work.id,
                            workname = work.name
                        };
                        ch.declare(fact.date.Month, fact.hours);
                    }
                }
                if (curwork == work)
                {
                    res.Add(ch);
                    ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                }      
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range";
        }


        /*
         * Возвращает количество затраченных на выполнений конкретной работы часов за каждый месяц указанного года.
         */
        /// <summary>
        /// Gives the number of hours spent on work for each month by ID of work.
        /// </summary>
        /// <returns></returns>
        [HttpGet("countWorkHoursById/{id}&&{year}")]
        public dynamic countWorkHoursById(int id, int year)
        {
            List<Count> res = new List<Count>();
            var work = db.works.SingleOrDefault(x => x.id == id);
            var curwork = new Works();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach (var fact in facts)
            {
                if (fact.workid == work.id && fact.date.Year == year)
                {
                    curwork = work;
                    ch.work = new
                    {
                        workid = work.id,
                        workname = work.name
                    };
                    ch.declare(fact.date.Month, fact.hours);
                }
            }
            if (curwork == work)
            {
                res.Add(ch);
                ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range";
        }


        /*
         *  Возвращает объем выполнения каждой работы за каждый месяц указанного года.
         *  Вывод значений производится лишь для тех работ, которые выполнялись хотя бы раз.
         */
        /// <summary>
        /// Gives the scope of work spent on work for each month.
        /// </summary>
        /// <returns></returns>
        [HttpGet("countAllWorksScope/{year}")]
        public dynamic countAllWorksScope(int year)
        {
            List<Count> res = new List<Count>();
            var works = db.works.ToList();
            var curwork = new Works();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach (var work in works)
            {
                foreach (var fact in facts)
                {
                    if (fact.workid == work.id && fact.date.Year == year)
                    {
                        curwork = work;
                        ch.work = new
                        {
                            workid = work.id,
                            workname = work.name
                        };
                        ch.declare(fact.date.Month, fact.count);
                    }
                }
                if (curwork == work)
                {
                    res.Add(ch);
                    ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                }
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range";
        }


        /*
         * Возвращает объем выполнения конкретной работы за каждый месяц указанного года.
         */
        /// <summary>
        /// Gives the scope of work spent on work for each month by ID of work.
        /// </summary>
        /// <returns></returns>
        [HttpGet("countWorkScopeById/{id}&&{year}")]
        public dynamic countWorkScopeById(int id, int year)
        {
            List<Count> res = new List<Count>();
            var work = db.works.SingleOrDefault(x => x.id == id);
            var curwork = new Works();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach (var fact in facts)
            {
                if (fact.workid == work.id && fact.date.Year == year)
                {
                    curwork = work;
                    ch.work = new
                    {
                        workid = work.id,
                        workname = work.name
                    };
                    ch.declare(fact.date.Month, fact.count);
                }
            }
            if (curwork == work)
            {
                res.Add(ch);
                ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range"; ;
        }


        /*
         * Возвращает количество затраченного на дорогу каждым работником времени за каждый месяц указанного года. 
         * Вывод значений производится лишь для тех работников, которые проделали хотя бы один маршрут.
         */
        /// <summary>
        /// Gives the time spent by the employees on the movement for each month.
        /// </summary>
        /// <returns></returns>
        [HttpGet("countMovingTimeAllEmployees/{year}")]
        public dynamic countMovingTimeAllEmployees(int year)
        {
            var relres = new List<dynamic>();
            var emps = db.employees.ToList();
            var curemp = new Employees();
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            TravelTime ch = new TravelTime(duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration);
            var routes = db.routes.ToList();
            foreach(var emp in emps)
            {
                foreach (var route in routes)
                {
                    var planid = db.employee_to_plan.SingleOrDefault(x => x.id == route.epid).planid;
                    var plan = db.plans.SingleOrDefault(x => x.id == planid);
                    if (plan.date.Year == year && db.employee_to_plan.SingleOrDefault(x => x.id == route.epid).employeeid == emp.id)
                    {
                        curemp = emp;
                        ch.employee = new
                        {
                            empid = emp.id,
                            empname = emp.fio
                        };
                        var routetime = route.etime.Subtract(route.stime);
                        ch.declare(plan.date.Month, routetime);
                    }
                }
                if (curemp == emp)
                {
                    var rres = new
                    {
                        employee = ch.employee,
                        months = new List<string>()
                    };
                    foreach (var monthtime in ch.months)
                    {
                        rres.months.Add(monthtime.ToString());
                    }
                    relres.Add(rres);
                    ch = new TravelTime(duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration);
                }
            }
            if (relres.Count > 0)
                return relres;
            else
                return "No data available for current year/date range"; ;
        }


        /*
         * Возвращает количество затраченного на дорогу конкретным работником времени за каждый месяц указанного года.
         */
        /// <summary>
        /// Gives the time spent by the employee on the movement for each month by ID of employee.
        /// </summary>
        /// <returns></returns>
        [HttpGet("countMovingTimeByEmployeeId/{id}&&{year}")]
        public dynamic countMovingTimeByEmployeeId(int id, int year)
        {
            var relres = new List<dynamic>();
            var emp = db.employees.SingleOrDefault(x => x.id == id);
            var curemp = new Employees();
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            TravelTime ch = new TravelTime(duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration);
            var routes = db.routes.ToList();
            foreach (var route in routes)
            {
                var planid = db.employee_to_plan.SingleOrDefault(x => x.id == route.epid).planid;
                var plan = db.plans.SingleOrDefault(x => x.id == planid);
                if (plan.date.Year == year && db.employee_to_plan.SingleOrDefault(x => x.id == route.epid).employeeid == emp.id)
                {
                    curemp = emp;
                    ch.employee = new
                    {
                        empid = emp.id,
                        empname = emp.fio
                    };
                    var routetime = route.etime.Subtract(route.stime);
                    ch.declare(plan.date.Month, routetime);
                }
            }
            if (curemp == emp)
            {
                var rres = new
                {
                    employee = ch.employee,
                    months = new List<string>()
                };
                foreach (var monthtime in ch.months)
                {
                    rres.months.Add(monthtime.ToString());
                }
                relres.Add(rres);
                ch = new TravelTime(duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration, duration);
            }
            if (relres.Count > 0)
                return relres;
            else
                return "No data available for current year/date range"; ;
        }
    }
}
