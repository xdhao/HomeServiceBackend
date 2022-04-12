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


        [HttpGet("countAllWorksHours/{year}")]
        public dynamic countAllWorksHours(int year)
        {
            List<Count> res = new List<Count>();
            var works = db.works.ToList();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach(var work in works)
            {
                foreach (var fact in facts)
                {
                    if (fact.workid == work.id && fact.date.Year == year)
                    {
                        ch.work = work;
                        ch.declare(fact.date.Month, fact.hours);
                    }
                }
                if (ch.work == work)
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

        [HttpGet("countWorkHoursById/{id}&&{year}")]
        public dynamic countWorkHoursById(int id, int year)
        {
            List<Count> res = new List<Count>();
            var work = db.works.SingleOrDefault(x => x.id == id);
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach (var fact in facts)
            {
                if (fact.workid == work.id && fact.date.Year == year)
                {
                    ch.work = work;
                    ch.declare(fact.date.Month, fact.hours);
                }
            }
            if (ch.work == work)
            {
                res.Add(ch);
                ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range";
        }

        [HttpGet("countAllWorksScope")]
        public dynamic countAllWorksScope()
        {
            List<Count> res = new List<Count>();
            var works = db.works.ToList();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach (var work in works)
            {
                foreach (var fact in facts)
                {
                    if (fact.workid == work.id)
                    {
                        ch.work = work;
                        ch.declare(fact.date.Month, fact.count);
                    }
                }
                if (ch.work == work)
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

        [HttpGet("countWorkScopeById/{id}")]
        public dynamic countWorkScopeById(int id)
        {
            List<Count> res = new List<Count>();
            var work = db.works.SingleOrDefault(x => x.id == id);
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach (var fact in facts)
            {
                if (fact.workid == work.id)
                {
                    ch.work = work;
                    ch.declare(fact.date.Month, fact.count);
                }
            }
            if (ch.work == work)
            {
                res.Add(ch);
                ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            if (res.Count > 0)
                return res;
            else
                return "No data available for current year/date range"; ;
        }
    }
}
