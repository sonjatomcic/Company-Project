using CompanyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();
        Employee GetById(int id);
        IQueryable<Employee> GetByBirthYear(int year);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        IQueryable<Employee> GetByEmploymentYear(EmployeeFilter filter);


    }
}
