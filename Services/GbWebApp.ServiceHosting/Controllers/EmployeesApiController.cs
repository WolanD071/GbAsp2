//using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IAnyEntityCRUD<Employee>
    {
        private readonly IAnyEntityCRUD<Employee> _employeesData;

        public EmployeesApiController(IAnyEntityCRUD<Employee> employeesData) => _employeesData = employeesData;

        [HttpPost]
        public int Add(Employee emp) => _employeesData.Add(emp);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);

        [HttpGet]
        public IEnumerable<Employee> Get() => _employeesData.Get();

        [HttpGet("{id}")]
        public Employee Get(int id) => _employeesData.Get(id);

        //[HttpPut("{id}")]
        [HttpPut]
        public void Update(/*int id,*/ Employee emp) => _employeesData.Update(emp);
    }
}
