using HRApplication_Wpf.Models.Configurations;
using HRApplication_Wpf.Models.Domains;
using HRApplication_Wpf.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace HRApplication_Wpf
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectionString = $@"
            Server={Settings.Default.ServerAddress}\{Settings.Default.ServerName};
            initial catalog={Settings.Default.DbName};
            user id={Settings.Default.UserName};
            password={Settings.Default.Password};";

        public ApplicationDbContext()
            :base(_connectionString)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("DateTime2"));
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new EmployeeTypeConfiguration());
        }

    }
}