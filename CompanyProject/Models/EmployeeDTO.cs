using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyProject.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int YearOfEmployment { get; set; }
        public decimal Salary { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}