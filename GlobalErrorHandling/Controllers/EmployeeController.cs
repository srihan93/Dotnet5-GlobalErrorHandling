using GlobalErrorHandling.Exceptions;
using GlobalErrorHandling.Extensions;
using GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GlobalErrorHandling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public static readonly List<Employee> employees = new List<Employee>()
        {
            new Employee(){Id=1,Name="Varun",Age=28},
             new Employee(){Id=2,Name="Aadhi",Age=26},
              new Employee(){Id=3,Name="Vikram",Age=26},
               new Employee(){Id=4,Name="Rani",Age=32},
                new Employee(){Id=5,Name="Tarwin",Age=34},
                 new Employee(){Id=6,Name="Docko",Age=30},
                  new Employee(){Id=7,Name="Vyasa",Age=23},
                   new Employee(){Id=8,Name="Moddy",Age=22},
                    new Employee(){Id=9,Name="Joddy",Age=25},
                   new Employee(){Id=10,Name="Shridha",Age=25},
                   new Employee(){Id=11,Name="Dhubu",Age=26}
        };

        public EmployeeController()
        {
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ServiceFilter(typeof(ValidateAttributeFilter))]
        public Employee GetById(int id)
        {
            var employee = employees.Where(e => e.Id == id).FirstOrDefault();
            if (employee == null)
            {
                throw new ItemNotFound("Employee :" + id + " was not found");
            }
            return employee;
        }
    }
}