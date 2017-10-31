using System;
using System.Data.SqlClient;
using System.Linq;

namespace SoftuniSystem
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftuniContext();

            //CallAStoredProcedure(context);

            //EmployeesMaximumSalaries(context);
        }

        private static void EmployeesMaximumSalaries(SoftuniContext context)
        {
            var departments = context.Departments
                .Select(d => new {d.Name, MaxSalary = d.Employees.Select(e => e.Salary).Max()})
                .Where(d => d.MaxSalary > 70000 || d.MaxSalary < 30000);

            foreach (var department in departments)
            {
                Console.WriteLine($"{department.Name} - {department.MaxSalary}");
            }
        }

        private static void CallAStoredProcedure(SoftuniContext context)
        {
            //var queryToCreateProcedure = @"CREATE PROCEDURE usp_EmployeeProjects (@firstName VARCHAR(50), @lastName VARCHAR(50))
            //AS
            //BEGIN

            //    SELECT * FROM Employees as e

            //        INNER JOIN EmployeesProjects as ep

            //            ON e.EmployeeID = ep.EmployeeID

            //        INNER JOIN Projects as p

            //            ON ep.ProjectID = p.ProjectID

            //        WHERE e.FirstName = @firstName AND e.LastName = @lastName
            //END";
            //context.Database.ExecuteSqlCommand(queryToCreateProcedure);
            //context.SaveChanges();

            var firstName = Console.ReadLine();
            var secondName = Console.ReadLine();

            var paramFirstName = new SqlParameter("@firstName", firstName);
            var paramSecondName = new SqlParameter("@secondName", secondName);

            var employeeProjects = context.Database
                .SqlQuery<Project>("EXEC dbo.usp_EmployeeProjects @firstName, @secondName",
                    paramFirstName, paramSecondName);

            foreach (Project p in employeeProjects)
            {
                Console.WriteLine($"{p.Name} - {p.Description}, {p.StartDate}");
            }
        }
    }
}
