namespace CompanyProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CompanyProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CompanyProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Companies.AddOrUpdate(c => c.Id,
                new Models.Company() { Id = 1, Name = "Google", Founded = 1998 },
                new Models.Company() { Id = 2, Name = "Apple", Founded = 1976 },
                new Models.Company() { Id = 3, Name = "Microsoft", Founded = 1975 }

                );

            context.Employees.AddOrUpdate(e => e.Id,
                new Models.Employee() { Id = 1, Name = "Pera Peric", BirthYear = 1980, YearOfEmployment = 2008, Salary = 3000, CompanyId = 1 },
                new Models.Employee() { Id = 2, Name = "Mika Mikic", BirthYear = 1976, YearOfEmployment = 2005, Salary = 6000, CompanyId = 1 },
                new Models.Employee() { Id = 3, Name = "Iva Ivic", BirthYear = 1990, YearOfEmployment = 2016, Salary = 4000, CompanyId = 2 },
                new Models.Employee() { Id = 4, Name = "Zika Zikic", BirthYear = 1985, YearOfEmployment = 2005, Salary = 5000, CompanyId = 2 },
                new Models.Employee() { Id = 5, Name = "Sara Saric", BirthYear = 1982, YearOfEmployment = 2007, Salary = 5500, CompanyId = 3 }

                );
        }
    }
}
