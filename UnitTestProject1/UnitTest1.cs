using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using CompanyProject.Controllers;
using CompanyProject.Interfaces;
using CompanyProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //get returns employee with same id
        [TestMethod]
        public void GetReturnsEmployeeWithSameId()
        {


            //arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var mockRepository2 = new Mock<ICompanyRepository>();

            mockRepository.Setup(x => x.GetById(42)).Returns(new Employee { Id = 42, Company = new Company { Id = 1 } });


            var controller = new EmployeesController(mockRepository.Object);

            //act

            System.Web.Http.IHttpActionResult actionResult = controller.GetEmployee(42);
            var contentResult = actionResult as OkNegotiatedContentResult<EmployeeDTO>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);

        }


        //put returns bad request
        [TestMethod]
        public void PutReturnsBadRequest()
        {
            //arrange
            var mockRepository = new Mock<IEmployeeRepository>();

            var controller = new EmployeesController(mockRepository.Object);

            //act

            Employee testEmployee = new Employee() { Id = 1, Name = "test test", BirthYear = 1990, YearOfEmployment = 2015, Salary = 1000, CompanyId = 1 };

            IHttpActionResult actionResult = controller.PutEmployee(10, testEmployee);
            var createdResult = actionResult as BadRequestResult;


            //assert
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        }


        //get returns multiple objects
        [TestMethod]
        public void GetReturnsMultipleObjects()
        {

            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, Name = "test test", BirthYear = 1990, YearOfEmployment = 2015, Salary = 1000, Company = new Company { Id = 1, Name = "kompanija1" } });

            employees.Add(new Employee { Id = 2, Name = "test test2", BirthYear = 1992, YearOfEmployment = 2015, Salary = 1000, Company = new Company { Id = 2, Name = "komopaniaja2" } });

            var mockRepository = new Mock<IEmployeeRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(employees.AsQueryable());
            var controller = new EmployeesController(mockRepository.Object);

            //act
            IQueryable<EmployeeDTO> result = controller.GetEmployees();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employees.Count, result.ToList().Count);

            Assert.AreEqual(employees.ElementAt(0).Id, result.ElementAt(0).Id);
            Assert.AreEqual(employees.ElementAt(0).Name, result.ElementAt(0).Name);

            Assert.AreEqual(employees.ElementAt(0).BirthYear, result.ElementAt(0).BirthYear);
            Assert.AreEqual(employees.ElementAt(0).YearOfEmployment, result.ElementAt(0).YearOfEmployment);
            Assert.AreEqual(employees.ElementAt(0).Company.Name, result.ElementAt(0).CompanyName);

            Assert.AreEqual(employees.ElementAt(1).Id, result.ElementAt(1).Id);
            Assert.AreEqual(employees.ElementAt(1).Name, result.ElementAt(1).Name);

            Assert.AreEqual(employees.ElementAt(1).BirthYear, result.ElementAt(1).BirthYear);
            Assert.AreEqual(employees.ElementAt(1).YearOfEmployment, result.ElementAt(1).YearOfEmployment);
            Assert.AreEqual(employees.ElementAt(1).Company.Name, result.ElementAt(1).CompanyName);



        }

        //post returns multiple objects
        [TestMethod]
        public void PostReturnsMultipleObjects()
        {

            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, Name = "test test", BirthYear = 1990, YearOfEmployment = 2008, Salary = 1000, Company = new Company { Id = 1, Name = "kompanija1" } });

            employees.Add(new Employee { Id = 2, Name = "test test2", BirthYear = 1992, YearOfEmployment = 2016, Salary = 1000, Company = new Company { Id = 2, Name = "komopaniaja2" } });

            EmployeeFilter filter = new EmployeeFilter() { StartYear = 2008, EndYear = 2016 };

            var mockRepository = new Mock<IEmployeeRepository>();
            mockRepository.Setup(x => x.GetByEmploymentYear(filter)).Returns(employees.AsQueryable());
            var controller = new EmployeesController(mockRepository.Object);

            //act
            IQueryable<EmployeeDTO> result = controller.PostByEmploymentYear(filter);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employees.Count, result.ToList().Count);

            Assert.AreEqual(employees.ElementAt(0).Id, result.ElementAt(0).Id);
            Assert.AreEqual(employees.ElementAt(0).Name, result.ElementAt(0).Name);

            Assert.AreEqual(employees.ElementAt(0).BirthYear, result.ElementAt(0).BirthYear);
            Assert.AreEqual(employees.ElementAt(0).YearOfEmployment, result.ElementAt(0).YearOfEmployment);
            Assert.AreEqual(employees.ElementAt(0).Company.Name, result.ElementAt(0).CompanyName);

            Assert.AreEqual(employees.ElementAt(1).Id, result.ElementAt(1).Id);
            Assert.AreEqual(employees.ElementAt(1).Name, result.ElementAt(1).Name);

            Assert.AreEqual(employees.ElementAt(1).BirthYear, result.ElementAt(1).BirthYear);
            Assert.AreEqual(employees.ElementAt(1).YearOfEmployment, result.ElementAt(1).YearOfEmployment);
            Assert.AreEqual(employees.ElementAt(1).Company.Name, result.ElementAt(1).CompanyName);



        }

    }
}
