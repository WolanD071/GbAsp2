//using System.Linq;
using System.Net.Http;
using GbWebApp.Interfaces;
using GbWebApp.Clients.Base;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace GbWebApp.Clients.Employees
{
    public class EmployeesClient : BaseClient, IAnyEntityCRUD<Employee>
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebApiRoutes.EmployeesAPI) { }

        public int Add(Employee emp) => Post(Address, emp).Content.ReadAsAsync<int>().Result;

        public bool Delete(int id) => Delete($"{Address}/{id}").IsSuccessStatusCode;

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public void Update(Employee emp) => Put(Address, emp);
    }
}
