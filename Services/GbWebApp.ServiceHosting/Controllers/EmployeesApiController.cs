using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    /// <summary> employees management </summary>
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IAnyEntityCRUD<Employee>
    {
        private readonly IAnyEntityCRUD<Employee> _employeesData;

        public EmployeesApiController(IAnyEntityCRUD<Employee> employeesData) => _employeesData = employeesData;

        /// <summary> adding an employee </summary>
        /// <param name="emp"> employee itself </param>
        /// <returns>id of the employee have been added </returns>
        [HttpPost]
        public int Add(Employee emp) => _employeesData.Add(emp);

        /// <summary> deletion of the employee with given id </summary>
        /// <param name="id"> id of the employee </param>
        /// <returns> bool - success or fail </returns>
        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);

        /// <summary> getting the all employee's list from database </summary>
        /// <returns> list of employees </returns>
        [HttpGet]
        public IEnumerable<Employee> Get() => _employeesData.Get();

        /// <summary> getting the employee by its id </summary>
        /// <param name="id"> given id </param>
        /// <returns> employee found </returns>
        [HttpGet("{id}")]
        public Employee Get(int id) => _employeesData.Get(id);

        /// <summary> updating info about the employee with given id </summary>
        /// <param name="emp">employee</param>
        [HttpPut]
        //[HttpPut("{id}")]
        public void Update(/*int id,*/ Employee emp) => _employeesData.Update(emp);
    }
}
