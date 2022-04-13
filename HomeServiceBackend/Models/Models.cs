using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    // Сущности БД
    public class Clients
    {
        public int id { get; set; }
        public string fio { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public bool deleted { get; set; }
    }

    public class Coordinates
    {
        public int id { get; set; }
        public int employeeid { get; set; }
        public DateTime time { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Employee_to_plan
    {
        public Employee_to_plan()
        {
        }
        public Employee_to_plan(int planid, int employeeid)
        {
            this.planid = planid;
            this.employeeid = employeeid;
        }
        public int id { get; set; }
        public int planid { get; set; }
        public int employeeid { get; set; }
    }

    public class Employees
    {
        public int id { get; set; }
        public int function_id { get; set; }
        public string fio { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public DateTime birthdate { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public bool deleted { get; set; }
    }

    public class Facts
    {
        public int id { get; set; }
        public int planid { get; set; }
        public int workid { get; set; }
        public int propertyid { get; set; }
        public DateTime date { get; set; }
        public float count { get; set; }
        public int number_of_people { get; set; }
        public int hours { get; set; }
        public bool deleted { get; set; }
    }

    public class Plans
    {
        public int id { get; set; }
        public int workid { get; set; }
        public int propertyid { get; set; }
        public DateTime date { get; set; }
        public float count { get; set; }
        public int number_of_people { get; set; }
        public int hours { get; set; }
        public bool deleted { get; set; }
    }

    public class Propertys
    {
        public int id { get; set; }
        public string adress { get; set; }
        public int area_size { get; set; }
        public int length { get; set; }
        public int width { get; set; }
        public int client_id { get; set; }
        public bool deleted { get; set; }
    }

    public class Reports
    {
        public int id { get; set; }
        public int epid { get; set; }
        public string photo_url { get; set; }
        public string comment { get; set; }
        public bool deleted { get; set; }
    }

    public class Routes
    {
        public int id { get; set; }
        public DateTime stime { get; set; }
        public DateTime etime { get; set; }
        public int epid { get; set; }
        public bool deleted { get; set; }
    }

    public class Types_of_work
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
    }

    public class Units
    {
        public int id { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public bool deleted { get; set; }
    }

    public class Works
    {
        public int id { get; set; }
        public string name { get; set; }
        public int number_of_people { get; set; }
        public int typesid { get; set; }
        public int unitid { get; set; }
        public int hours { get; set; }
        public bool deleted { get; set; }
    }
    public class Employees_functions
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
    }
}