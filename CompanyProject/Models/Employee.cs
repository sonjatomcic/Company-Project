using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(1952, 1990)]
        public int BirthYear { get; set; }
        [Required]
        [Range(2001, int.MaxValue)]
        public int YearOfEmployment { get; set; }
        [Required]
        [Range(2001, 9999)]
        public decimal Salary { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}