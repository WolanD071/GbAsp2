using System.Net.Http;
using GbWebApp.Interfaces;
using GbWebApp.Clients.Base;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Clients.Employees
{
    public class EmployeesClient : BaseClient, IAnyEntityCRUD<Employee>
    {
        private readonly ILogger<EmployeesClient> _logger;

        public EmployeesClient(IConfiguration cfg, ILogger<EmployeesClient> logger) : base(cfg, WebApiRoutes.EmployeesAPI) => _logger = logger;

        public int Add(Employee emp)
        {
            _logger.LogInformation("Creating new employee...");
            using (_logger.BeginScope("*** CREATING EMPLOYEE SCOPE ***"))
            {
                var result = Post(Address, emp).Content.ReadAsAsync<int>().Result;
                _logger.LogInformation($"...completed successfully! id={result}");
                return result;
            }
        }

        public bool Delete(int id)
        {
            _logger.LogInformation($"Deleting the employee with id={id}...");
            using (_logger.BeginScope("*** DELETING EMPLOYEE SCOPE ***"))
            {
                var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
                _logger.LogInformation("{0}",result ? "...completed successfully!" : "such an employee not found!");
                return result;
            }
        }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public void Update(Employee emp)
        {
            _logger.LogInformation($"Updating the employee with id={emp.Id}...");
            using (_logger.BeginScope("*** UPDATING EMPLOYEE SCOPE ***"))
            {
                Put(Address, emp);
                _logger.LogInformation("...completed successfully!");
            }
        }
    }
}
