using HRApplication_Wpf.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication_Wpf.Models.Configurations
{
    internal class EmployeeTypeConfiguration : EntityTypeConfiguration<EmployeeType>
    {
        public EmployeeTypeConfiguration() 
        {
            ToTable("dbo.EmployeeTypes");

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Name)
                .HasMaxLength(50);
        }
    }
}
