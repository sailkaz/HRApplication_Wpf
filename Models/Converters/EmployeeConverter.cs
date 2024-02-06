using HRApplication_Wpf.Models.Domains;
using HRApplication_Wpf.Models.Wrappers;

namespace HRApplication_Wpf.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee employee) 
        {
            return new EmployeeWrapper
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Comments = employee.Comments,
                EmploymentDate = (System.DateTime)employee.EmploymentDate,
                DismissDate = (System.DateTime)employee.DismissDate,
                HourlyRate = employee.HourlyRate,
                EmployeeType = new EmployeeTypeWrapper
                {
                    Id = employee.EmployeeType.Id,
                    Name = employee.EmployeeType.Name
                }
            };
        }

        public static Employee ToEmployeeDao(this EmployeeWrapper employeeWrapper) 
        {
            return new Employee
            {
                Id = employeeWrapper.Id,
                FirstName = employeeWrapper.FirstName,
                LastName = employeeWrapper.LastName,
                Comments = employeeWrapper.Comments,
                EmploymentDate = employeeWrapper.EmploymentDate,
                DismissDate = employeeWrapper.DismissDate,
                HourlyRate = employeeWrapper.HourlyRate,
                EmployeeTypeId = employeeWrapper.EmployeeType.Id
            };
        }

    }
}
