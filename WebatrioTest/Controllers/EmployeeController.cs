using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using WebatrioTest.Helpers;
using WebatrioTest.Models;

namespace WebatrioTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>()
        {
            new Employee
            {
                Id = 1,
                FirstName = "Katy",
                LastName = "Perry",
                DateOfBirth = new DateTime(1984, 10, 25),
                CurrentJob = "Software Developper",
                PreviousJobs = new List<string>() {"Manager", "Intern"},
                CurrentCompany = new Company {CompanyId = 1, NameCompany = "Microsoft"},
                PreviousCompanies = new List<Company>
                {
                    new Company {CompanyId = 1, NameCompany = "Apple"},
                    new Company {CompanyId = 1, NameCompany = "Amazon"}
                },
                StartDate = new DateTime(2020, 1, 1),
                EndDate = null,
            },
            new Employee
            {
                Id = 2,
                FirstName = "Billy",
                LastName = "Paul",
                DateOfBirth = new DateTime(1934, 12, 1),
                CurrentJob = "System Administrator",
                PreviousJobs = new List<string>() {"Singer", "Compositor"},
                CurrentCompany = new Company {CompanyId = 1, NameCompany = "Apple"},
                PreviousCompanies = new List<Company>
                {
                    new Company {CompanyId = 1, NameCompany = "Facebook"}
                },
                StartDate = new DateTime(2020, 1, 1),
                EndDate = null,
            },
            new Employee
            {
                Id = 3,
                FirstName = "Martin",
                LastName = "Solveig",
                DateOfBirth = new DateTime(1976, 9, 22),
                CurrentJob = "Devops Engineer",
                PreviousJobs = new List<string>() {"CEO", "Software Developper"},
                CurrentCompany = new Company {CompanyId = 1, NameCompany = "Amazon"},
                PreviousCompanies = new List<Company>{},
                StartDate = new DateTime(2023, 1, 1),
                EndDate = null,
            }
        };


        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetEmployees()
        {
            return Ok(_employees.OrderBy(e => e.LastName));
        }


        [HttpGet]
        [Route("GetEmployeesByCompanyName")]
        public IActionResult GetEmployeesByCompanyName([FromQuery] string companyName)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                return BadRequest("Le nom de l'entreprise est manquant.");
            }

            var matchingEmployees = _employees.Where(e =>
                e.CurrentCompany != null && e.CurrentCompany.NameCompany.ToLower() == companyName.ToLower() ||
                e.PreviousCompanies != null && e.PreviousCompanies.Any(c => c.NameCompany.ToLower() == companyName.ToLower())
            ).ToList();

            return Ok(matchingEmployees);
        }


        [HttpPost]
        [Route("AddNewEmployee")]
        public IActionResult AddNewEmployee(Employee employee)
        {
            int age = AgeCalculator.CalculerAge(employee.DateOfBirth);

            if (age < 150)
            {
                _employees.Add(employee);
                return Ok();
            }
            else
            {
                return BadRequest("La personne a plus de 150 ans, elle ne peut pas être ajoutée.");
            }
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == employee.Id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.CurrentJob = employee.CurrentJob;
            existingEmployee.StartDate = employee.StartDate;
            existingEmployee.EndDate = employee.EndDate;

            return NoContent();
        }


        //[HttpDelete]
        //[Route("DeleteEmployee")]
        //public IActionResult DeleteEmployee(int id)
        //{
        //    var employee = _employees.FirstOrDefault(e => e.Id == id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _employees.Remove(employee);

        //    return NoContent();
        //}


    }
}
