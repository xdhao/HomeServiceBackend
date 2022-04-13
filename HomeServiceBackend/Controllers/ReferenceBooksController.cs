﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeServiceBackend.Models;


namespace HomeServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceBooksController : ControllerBase
    {
        private ApplicationContext db;
        public ReferenceBooksController(ApplicationContext context)
        {
            db = context;
        }

        // Справочники - CRUD

        // READ by id
        [HttpGet("getClientById/{id}")]
        public Clients getClientById(int id)
        {
            return db.clients.SingleOrDefault(x => x.id == id);
        }

        [HttpGet("getEmployeeById/{id}")]
        public dynamic getEmployeeById(int id)
        {
            var res = new {
                Employee = db.employees.SingleOrDefault(x => x.id == id),
                func_name = db.employees_functions.SingleOrDefault(x => x.id == db.employees.SingleOrDefault(x => x.id == id).function_id).name
            };
            return res;
        }

        [HttpGet("getPropertyById/{id}")]
        public dynamic getPropertyById(int id)
        {
            var res = new
            {
                Property = db.propertys.SingleOrDefault(x => x.id == id),
                client = db.clients.SingleOrDefault(x => x.id == db.propertys.SingleOrDefault(x => x.id == id).client_id)
            };
            return res;
        }

        [HttpGet("getUnitById/{id}")]
        public Units getUnitById(int id)
        {
            return db.units.SingleOrDefault(x => x.id == id);
        }

        [HttpGet("getTypeofWorkById/{id}")]
        public Types_of_work getTypeofWorkById(int id)
        {
            return db.types_of_work.SingleOrDefault(x => x.id == id);
        }

        [HttpGet("getWorkById/{id}")]
        public dynamic getWorkById(int id)
        {
            var res = new
            {
                Work = db.works.SingleOrDefault(x => x.id == id),
                type_name = db.types_of_work.SingleOrDefault(x => x.id == db.works.SingleOrDefault(x => x.id == id).typesid).name,
                unit_name = db.units.SingleOrDefault(x => x.id == db.works.SingleOrDefault(x => x.id == id).unitid).name
            };
            return res;
        }

        [HttpGet("getFunctionById/{id}")]
        public Employees_functions getFunctionById(int id)
        {
            return db.employees_functions.SingleOrDefault(x => x.id == id);
        }

        // READ
        [HttpGet("getAllClients")]
        public IEnumerable<Clients> getAllClients()
        {
            return db.clients;
        }

        [HttpGet("getAllEmployees")]
        public dynamic getAllEmployees()
        {
            var emps = db.employees.ToList();
            var res = new List<dynamic>();
            foreach (var emp in emps)
            {
                var re = new
                {
                    Employee = db.employees.SingleOrDefault(x => x.id == emp.id),
                    func_name = db.employees_functions.SingleOrDefault(x => x.id == db.employees.SingleOrDefault(x => x.id == emp.id).function_id).name
                };
                res.Add(re);
            }

            return res;
        }

        [HttpGet("getAllPropertys")]
        public dynamic getAllPropertys()
        {
            var propes = db.propertys.ToList();
            var res = new List<dynamic>();
            foreach (var pro in propes)
            {
                var re = new
                {
                    Property = db.propertys.SingleOrDefault(x => x.id == pro.id),
                    client = db.clients.SingleOrDefault(x => x.id == db.propertys.SingleOrDefault(x => x.id == pro.id).client_id)
                };
                res.Add(re);
            }
            return res;
        }

        [HttpGet("getAllUnits")]
        public IEnumerable<Units> getAllUnits()
        {
            return db.units;
        }

        [HttpGet("getAllTypesofWork")]
        public IEnumerable<Types_of_work> getAllTypesofWork()
        {
            return db.types_of_work;
        }

        [HttpGet("getAllWorks")]
        public dynamic getAllWorks()
        {
            var works = db.works.ToList();
            var res = new List<dynamic>();
            foreach (var wr in works)
            {
                var re = new
                {
                    Work = db.works.SingleOrDefault(x => x.id == wr.id),
                    type_name = db.types_of_work.SingleOrDefault(x => x.id == db.works.SingleOrDefault(x => x.id == wr.id).typesid).name,
                    unit_name = db.units.SingleOrDefault(x => x.id == db.works.SingleOrDefault(x => x.id == wr.id).unitid).name
                };
                res.Add(re);
            }
            return res;
        }

        [HttpGet("getAllFunctions")]
        public IEnumerable<Employees_functions> getAllFunctions()
        {
            return db.employees_functions;
        }

        // CREATE
        [HttpPost("addClient")]
        public void addClient([FromBody] Clients client)
        {
            client.fio = client.surname + ' ' + client.name + ' ' + client.patronymic;
            db.clients.Add(client);
            db.SaveChanges();
        }

        [HttpPost("addEmployee")]
        public void addEmployee([FromBody] Employees employee)
        {
            employee.fio = employee.surname + ' ' + employee.name + ' ' + employee.patronymic;
            db.employees.Add(employee);
            db.SaveChanges();
        }

        [HttpPost("addProperty")]
        public void addProperty([FromBody] Propertys property)
        {
            db.propertys.Add(property);
            db.SaveChanges();
        }

        [HttpPost("addUnit")]
        public void addUnit([FromBody] Units unit)
        {
            db.units.Add(unit);
            db.SaveChanges();
        }

        [HttpPost("addTypeofWork")]
        public void addTypeofWork([FromBody] Types_of_work type_of_work)
        {
            db.types_of_work.Add(type_of_work);
            db.SaveChanges();
        }

        [HttpPost("addWork")]
        public void addWork([FromBody] Works work)
        {
            db.works.Add(work);
            db.SaveChanges();
        }

        [HttpPost("addFunction")]
        public void addFunction([FromBody] Employees_functions employee_function)
        {
            db.employees_functions.Add(employee_function);
            db.SaveChanges();
        }

        // UPDATE 
        [HttpPut("updateClient/{id}")]
        public void updateClient(int id, [FromBody] Clients client)
        {
            client.id = id;
            db.clients.Update(client);
            db.SaveChanges();
        }

        [HttpPut("updateEmployee/{id}")]
        public void updateEmployee(int id, [FromBody] Employees employee)
        {
            employee.id = id;
            db.employees.Update(employee);
            db.SaveChanges();
        }

        [HttpPut("updateProperty/{id}")]
        public void updateProperty(int id, [FromBody] Propertys property)
        {
            property.id = id;
            db.propertys.Update(property);
            db.SaveChanges();
        }

        [HttpPut("updateUnit/{id}")]
        public void updateUnit(int id, [FromBody] Units unit)
        {
            unit.id = id;
            db.units.Update(unit);
            db.SaveChanges();
        }

        [HttpPut("updateTypeofWork/{id}")]
        public void updateTypeofWork(int id, [FromBody] Types_of_work type_of_work)
        {
            type_of_work.id = id;
            db.types_of_work.Update(type_of_work);
            db.SaveChanges();
        }

        [HttpPut("updateWork/{id}")]
        public void updateWork(int id, [FromBody] Works work)
        {
            work.id = id;
            db.works.Update(work);
            db.SaveChanges();
        }

        [HttpPut("updateFunction/{id}")]
        public void updateFunction(int id, [FromBody] Employees_functions employee_function)
        {
            employee_function.id = id;
            db.employees_functions.Update(employee_function);
            db.SaveChanges();
        }


        // DELETE
        [HttpDelete("deleteClient/{id}")]
        public void deleteClient(int id)
        {
            var item = db.clients.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.clients.Remove(item);
                db.SaveChanges();
            }
        }

        [HttpDelete("deleteEmployee/{id}")]
        public void deleteEmployee(int id)
        {
            var item = db.employees.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.employees.Remove(item);
                db.SaveChanges();
            }
        }

        [HttpDelete("deleteProperty/{id}")]
        public void deleteProperty(int id)
        {
            var item = db.propertys.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.propertys.Remove(item);
                db.SaveChanges();
            }
        }

        [HttpDelete("deleteUnit/{id}")]
        public void deleteUnit(int id)
        {
            var item = db.units.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.units.Remove(item);
                db.SaveChanges();
            }
        }

        [HttpDelete("deleteTypeofWork/{id}")]
        public void deleteTypeofWork(int id)
        {
            var item = db.types_of_work.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.types_of_work.Remove(item);
                db.SaveChanges();
            }
        }

        [HttpDelete("deleteWork/{id}")]
        public void deleteWork(int id)
        {
            var item = db.works.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.works.Remove(item);
                db.SaveChanges();
            }
        }

        [HttpDelete("deleteFunction/{id}")]
        public void deleteFunction(int id)
        {
            var item = db.employees_functions.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.employees_functions.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
