using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeServiceBackend.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Clients> clients { get; set; }
        public DbSet<Propertys> propertys { get; set; }
        public DbSet<Units> units { get; set; }
        public DbSet<Types_of_work> types_of_work { get; set; }
        public DbSet<Works> works { get; set; }
        public DbSet<Plans> plans { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<Facts> facts { get; set; }
        public DbSet<Employee_to_plan> employee_to_plan { get; set; }
        public DbSet<Reports> reports { get; set; }
        public DbSet<Coordinates> coordinates { get; set; }
        public DbSet<Routes> routes { get; set; }
        public DbSet<Employees_functions> employees_functions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
