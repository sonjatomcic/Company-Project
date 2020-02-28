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
    public class EmployeesController : ApiController
    {
        IEmployeeRepository repository { get; set; }

        public EmployeesController(IEmployeeRepository repo)
        {
            repository = repo;
        }

        //GET api/employees
        public IQueryable<EmployeeDTO> GetEmployees()
        {
            var employees = from e in repository.GetAll()
                            select new EmployeeDTO
                            {
                                Id = e.Id,
                                Name = e.Name,
                                BirthYear = e.BirthYear,
                                YearOfEmployment = e.YearOfEmployment,
                                Salary = e.Salary,
                                CompanyName = e.Company.Name,
                                CompanyId = e.Company.Id
                            };
            return employees;
        }

        //GET api/employees/1
        [ResponseType(typeof(EmployeeDTO))]
        public IHttpActionResult GetEmployee(int id)
        {
            var e = repository.GetById(id);
            if (e == null)
            {
                return NotFound();
            }

            var employee = new EmployeeDTO()
            {
                Id = e.Id,
                Name = e.Name,
                BirthYear = e.BirthYear,
                YearOfEmployment = e.YearOfEmployment,
                Salary = e.Salary,
                CompanyName = e.Company.Name,
                CompanyId = e.Company.Id
            };

            return Ok(employee);
        }

        //GET api/employees/?year=1985
        public IQueryable<EmployeeDTO> GetByBirthYear(int year)
        {
            var employees = from e in repository.GetByBirthYear(year)
                            select new EmployeeDTO
                            {
                                Id = e.Id,
                                Name = e.Name,
                                BirthYear = e.BirthYear,
                                YearOfEmployment = e.YearOfEmployment,
                                Salary = e.Salary,
                                CompanyName = e.Company.Name,
                                CompanyId = e.Company.Id
                            };
            return employees;
        }

        //POST api/employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Add(employee);
            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);

        }

        //PUT api/employees/1
        [Authorize]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                repository.Update(employee);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(employee);

        }

        //DELETE api/employees/1
        [Authorize]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employee = repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            repository.Delete(employee);
            return Ok();

        }

        //POST api/employment
        [Authorize]
        [Route("api/employment")]
        public IQueryable<EmployeeDTO> PostByEmploymentYear(EmployeeFilter filter)
        {
            var employees = from e in repository.GetByEmploymentYear(filter)
                            select new EmployeeDTO
                            {
                                Id = e.Id,
                                Name = e.Name,
                                BirthYear = e.BirthYear,
                                YearOfEmployment = e.YearOfEmployment,
                                Salary = e.Salary,
                                CompanyName = e.Company.Name,
                                CompanyId = e.Company.Id
                            };
            return employees;
        }


    }
}
