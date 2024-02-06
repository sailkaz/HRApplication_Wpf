using System;

namespace HRApplication_Wpf.Models.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public DateTime? EmploymentDate { get; set; } 
        public DateTime? DismissDate { get; set; }
        public decimal HourlyRate { get; set; }
        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}