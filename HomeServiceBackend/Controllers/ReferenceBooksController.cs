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
    public class ReferenceBooksController : ControllerBase
    {
        private ApplicationContext db;
        public ReferenceBooksController(ApplicationContext context)
        {
            db = context;
        }

        // Справочники - CRUD

        // READ by id

        /// <summary>
        /// Gives client data by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getClientById/{id}")]
        public Clients getClientById(int id)
        {
            return db.clients.SingleOrDefault(x => x.id == id);
        }

        /// <summary>
        /// Gives employee data by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEmployeeById/{id}")]
        public dynamic getEmployeeById(int id)
        {
            var res = new {
                Employee = db.employees.SingleOrDefault(x => x.id == id),
                func_name = db.employees_functions.SingleOrDefault(x => x.id == db.employees.SingleOrDefault(x => x.id == id).function_id).name
            };
            return res;
        }

        /// <summary>
        /// Gives property data by his ID.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gives unit data by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUnitById/{id}")]
        public Units getUnitById(int id)
        {
            return db.units.SingleOrDefault(x => x.id == id);
        }

        /// <summary>
        /// Gives type of work data by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTypeofWorkById/{id}")]
        public Types_of_work getTypeofWorkById(int id)
        {
            return db.types_of_work.SingleOrDefault(x => x.id == id);
        }

        /// <summary>
        /// Gives work data by his ID.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gives function data by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getFunctionById/{id}")]
        public Employees_functions getFunctionById(int id)
        {
            return db.employees_functions.SingleOrDefault(x => x.id == id);
        }

        // READ

        /// <summary>
        /// Gives all clients data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllClients")]
        public IEnumerable<Clients> getAllClients()
        {
            return db.clients.Where(x => x.deleted == false);
        }

        /// <summary>
        /// Gives all employees data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllEmployees")]
        public dynamic getAllEmployees()
        {
            var emps = db.employees.Where(x => x.deleted == false).ToList();
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

        /// <summary>
        /// Gives all propertys data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllPropertys")]
        public dynamic getAllPropertys()
        {
            var propes = db.propertys.Where(x => x.deleted == false).ToList();
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

        /// <summary>
        /// Gives all units data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllUnits")]
        public IEnumerable<Units> getAllUnits()
        {
            return db.units.Where(x => x.deleted == false);
        }

        /// <summary>
        /// Gives all types of work data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllTypesofWork")]
        public IEnumerable<Types_of_work> getAllTypesofWork()
        {
            return db.types_of_work.Where(x => x.deleted == false);
        }

        /// <summary>
        /// Gives all works data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllWorks")]
        public dynamic getAllWorks()
        {
            var works = db.works.Where(x => x.deleted == false).ToList();
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

        /// <summary>
        /// Gives all functions data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllFunctions")]
        public IEnumerable<Employees_functions> getAllFunctions()
        {
            return db.employees_functions.Where(x => x.deleted == false);
        }

        // CREATE

        /// <summary>
        /// Create new client.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addClient")]
        public void addClient([FromBody] Clients client)
        {
            client.fio = client.surname + ' ' + client.name + ' ' + client.patronymic;
            db.clients.Add(client);
            db.SaveChanges();
        }

        /// <summary>
        /// Create new employee.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addEmployee")]
        public void addEmployee([FromBody] Employees employee)
        {
            employee.fio = employee.surname + ' ' + employee.name + ' ' + employee.patronymic;
            db.employees.Add(employee);
            db.SaveChanges();
        }

        /// <summary>
        /// Create new property.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addProperty")]
        public void addProperty([FromBody] Propertys property)
        {
            db.propertys.Add(property);
            db.SaveChanges();
        }

        /// <summary>
        /// Create new unit.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addUnit")]
        public void addUnit([FromBody] Units unit)
        {
            db.units.Add(unit);
            db.SaveChanges();
        }

        /// <summary>
        /// Create new type of work.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addTypeofWork")]
        public void addTypeofWork([FromBody] Types_of_work type_of_work)
        {
            db.types_of_work.Add(type_of_work);
            db.SaveChanges();
        }

        /// <summary>
        /// Create new work.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addWork")]
        public void addWork([FromBody] Works work)
        {
            db.works.Add(work);
            db.SaveChanges();
        }

        /// <summary>
        /// Create new function.
        /// </summary>
        /// <returns></returns>
        [HttpPost("addFunction")]
        public void addFunction([FromBody] Employees_functions employee_function)
        {
            db.employees_functions.Add(employee_function);
            db.SaveChanges();
        }

        /// <summary>
        /// Update client by his ID.
        /// </summary>
        /// <returns></returns>
        // UPDATE 
        [HttpPut("updateClient/{id}")]
        public void updateClient(int id, [FromBody] Clients client)
        {
            client.id = id;
            client.fio = client.surname + ' ' + client.name + ' ' + client.patronymic;
            db.clients.Update(client);
            db.SaveChanges();
        }

        /// <summary>
        /// Update employee by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateEmployee/{id}")]
        public void updateEmployee(int id, [FromBody] Employees employee)
        {
            employee.id = id;
            employee.fio = employee.surname + ' ' + employee.name + ' ' + employee.patronymic;
            db.employees.Update(employee);
            db.SaveChanges();
        }

        /// <summary>
        /// Update property by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateProperty/{id}")]
        public void updateProperty(int id, [FromBody] Propertys property)
        {
            property.id = id;
            db.propertys.Update(property);
            db.SaveChanges();
        }

        /// <summary>
        /// Update unit by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateUnit/{id}")]
        public void updateUnit(int id, [FromBody] Units unit)
        {
            unit.id = id;
            db.units.Update(unit);
            db.SaveChanges();
        }

        /// <summary>
        /// Update type of work by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateTypeofWork/{id}")]
        public void updateTypeofWork(int id, [FromBody] Types_of_work type_of_work)
        {
            type_of_work.id = id;
            db.types_of_work.Update(type_of_work);
            db.SaveChanges();
        }

        /// <summary>
        /// Update work by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateWork/{id}")]
        public void updateWork(int id, [FromBody] Works work)
        {
            work.id = id;
            db.works.Update(work);
            db.SaveChanges();
        }

        /// <summary>
        /// Update function by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateFunction/{id}")]
        public void updateFunction(int id, [FromBody] Employees_functions employee_function)
        {
            employee_function.id = id;
            db.employees_functions.Update(employee_function);
            db.SaveChanges();
        }


        // DELETE

        /// <summary>
        /// Delete client by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteClient/{id}")]
        public void deleteClient(int id)
        {
            var item = db.clients.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.clients.Remove(item);
                db.clients.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete employee by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteEmployee/{id}")]
        public void deleteEmployee(int id)
        {
            var item = db.employees.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.employees.Remove(item);
                db.employees.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete property by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteProperty/{id}")]
        public void deleteProperty(int id)
        {
            var item = db.propertys.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.propertys.Remove(item);
                db.propertys.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete unit by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteUnit/{id}")]
        public void deleteUnit(int id)
        {
            var item = db.units.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.units.Remove(item);
                db.units.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete type of work by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteTypeofWork/{id}")]
        public void deleteTypeofWork(int id)
        {
            var item = db.types_of_work.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.types_of_work.Remove(item);
                db.types_of_work.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete work by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteWork/{id}")]
        public void deleteWork(int id)
        {
            var item = db.works.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.works.Remove(item);
                db.works.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete function by his ID.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteFunction/{id}")]
        public void deleteFunction(int id)
        {
            var item = db.employees_functions.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.employees_functions.Remove(item);
                db.employees_functions.FirstOrDefault(x => x.id == id).deleted = true;
                db.SaveChanges();
            }
        }
    }
}
