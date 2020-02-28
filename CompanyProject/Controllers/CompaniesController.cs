using CompanyProject.Interfaces;
using CompanyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CompanyProject.Controllers
{
    public class CompaniesController : ApiController
    {
        ICompanyRepository repository { get; set; }

        public CompaniesController(ICompanyRepository repo)
        {
            repository = repo;
        }

        //GET api/companies
        public IQueryable<Company> GetCompanies()
        {
            return repository.GetAll();
        }

        //GET api/companies/1
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            var company = repository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        //GET api/tradition
        [Route("api/tradition")]
        public IQueryable<Company> GetTradition()
        {
            return repository.GetTradition();
        }

        //GET api/statistics
        [Route("api/statistics")]
        public IQueryable<CompanyDTO> GetStatistics()
        {
            return repository.GetStatistics();
        }

    }
}
