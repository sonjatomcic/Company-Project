using CompanyProject.Interfaces;
using CompanyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CompanyProject.Repository
{
    public class CompanyRepository : IDisposable, ICompanyRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        public IQueryable<Company> GetAll()
        {
            return db.Companies;
        }

        public Company GetById(int id)
        {
            return db.Companies.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<CompanyDTO> GetStatistics()
        {
            var employees = db.Employees.Include(e => e.Company);
            var companies = employees.GroupBy(e => e.Company, e => e.Salary, (company, salary) =>
            new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                Founded = company.Founded,
                AverageSalary = salary.Average()
            });

            var retVal = companies.OrderByDescending(c => c.AverageSalary);
            return retVal;

        }

        public IQueryable<Company> GetTradition()
        {
            List<Company> listYoung = db.Companies.OrderByDescending(c => c.Founded).ToList();
            List<Company> listOld = db.Companies.OrderBy(c => c.Founded).ToList();
            List<Company> retVal = new List<Company>();
            retVal.Add(listOld[0]);
            retVal.Add(listYoung[0]);
            return retVal.AsQueryable();
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