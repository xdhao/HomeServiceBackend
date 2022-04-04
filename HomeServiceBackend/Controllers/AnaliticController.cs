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

        [HttpGet("countAllWorksHours")]
        public IEnumerable<Count> countAllWorksHours()
        {
            List<Count> res = new List<Count>();
            var works = db.works.ToList();
            Count ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var facts = db.facts.ToList();
            foreach(var work in works)
            {
                foreach (var fact in facts)
                {
                    if (fact.workid == work.id)
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
            return res;
        }

        [HttpGet("countWorkHoursById/{id}")]
        public IEnumerable<Count> countWorkHoursById(int id)
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
                    ch.declare(fact.date.Month, fact.hours);
                }
            }
            if (ch.work == work)
            {
                res.Add(ch);
                ch = new Count(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            return res;
        }

        [HttpGet("countAllWorksScope")]
        public IEnumerable<Count> countAllWorksScope()
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
            return res;
        }

        [HttpGet("countWorkScopeById/{id}")]
        public IEnumerable<Count> countWorkScopeById(int id)
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
            return res;
        }
    }
}
