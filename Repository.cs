using HRApplication_Wpf.Models.Converters;
using HRApplication_Wpf.Models.Domains;
using HRApplication_Wpf.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace HRApplication_Wpf
{
    public class Repository
    {
        public List<EmployeeType> GetEmployeeTypes()
        {
            using(var _context = new ApplicationDbContext())
            {
                return _context.EmployeeTypes.ToList();
            }
        }

        public List<EmployeeWrapper> GetEmployees(int employeeTypeId)
        {
            using (var _context = new ApplicationDbContext())
            {
                var employees = _context.Employees
                    .Include(x => x.EmployeeType)
                    .AsQueryable();

                if (employeeTypeId != 0) 
                { 
                    employees = employees
                        .Where(x => x.EmployeeTypeId == employeeTypeId);
                }

                var employeesToReturn = new List<EmployeeWrapper>();
                foreach (var employee in employees) 
                {
                    var employeeToAdd = employee.ToWrapper();
                    employeesToReturn.Add(employeeToAdd);
                }

                return employeesToReturn;
            };
        }

        public void AddEmployee(EmployeeWrapper employee)
        {
            var employeeToAdd = employee.ToEmployeeDao();

            using(var _context = new ApplicationDbContext()) 
            {
                _context.Employees.Add(employeeToAdd);
                _context.SaveChanges();
            }
        }

        public void UpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            var employeeDao = employeeWrapper.ToEmployeeDao();

            using(var _context = new ApplicationDbContext())
            {
                var employeeToUpdate = _context.Employees.Find(employeeDao.Id);

                employeeToUpdate.FirstName = employeeDao.FirstName;
                employeeToUpdate.LastName = employeeDao.LastName;
                employeeToUpdate.Comments = employeeDao.Comments;
                employeeToUpdate.EmploymentDate = employeeDao.EmploymentDate;
                employeeToUpdate.DismissDate = employeeDao.DismissDate;
                employeeToUpdate.HourlyRate = employeeDao.HourlyRate;
                employeeToUpdate.EmployeeTypeId = employeeDao.EmployeeTypeId;

                _context.SaveChanges();               
            }
        }

    }
}
