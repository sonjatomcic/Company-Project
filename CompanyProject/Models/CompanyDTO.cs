using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyProject.Models
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Founded { get; set; }
        public decimal AverageSalary { get; set; }
    }
}