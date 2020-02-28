using CompanyProject.Interfaces;
using CompanyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CompanyProject.Repository
{
    public class EmployeeRepository : IDisposable, IEmployeeRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Employee employee)
        {
            //find company
            var company = db.Companies.FirstOrDefault(c => c.Id == employee.CompanyId);
            employee.Company = company;

            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }
     

        public IQueryable<Employee> GetAll()
        {
            return db.Employees.Include(e => e.Company).OrderByDescending(e => e.Salary);
        }

        public IQueryable<Employee> GetByBirthYear(int year)
        {
            return db.Employees.Include(e => e.Company).Where(e => e.BirthYear > year).OrderBy(e => e.BirthYear);
        }

        public IQueryable<Employee> GetByEmploymentYear(EmployeeFilter filter)
        {
            return db.Employees.Include(e => e.Company).Where(e => e.YearOfEmployment >= filter.StartYear && e.YearOfEmployment <= filter.EndYear).OrderBy(e => e.YearOfEmployment);
        }

        public Employee GetById(int id)
        {
            return db.Employees.Include(e => e.Company).FirstOrDefault(e => e.Id == id);
        }

        public void Update(Employee employee)
        {
            //find company
            var company = db.Companies.FirstOrDefault(c => c.Id == employee.CompanyId);
            employee.Company = company;

            db.Entry(employee).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}