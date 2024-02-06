using HRApplication_Wpf.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication_Wpf.Models.Configurations
{
    internal class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration() 
        {
            ToTable("dbo.Employees");

            HasKey(x => x.Id);
            Property(x => x.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            Property(x => x.LastName)
                .HasMaxLength(30) 
                .IsRequired();
        }
    }
}
