using CompanyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Interfaces
{
    public interface ICompanyRepository
    {
        IQueryable<Company> GetAll();
        Company GetById(int id);
        IQueryable<Company> GetTradition();
        IQueryable<CompanyDTO> GetStatistics();

    }
}
