using System;
using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace GbWebApp.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        readonly IAnyEntityCRUD<Employee> __employeesData;

        public EmployeesController(IAnyEntityCRUD<Employee> employeesData) { __employeesData = employeesData; }

        public ActionResult Index() =>
            (__employeesData == null) ?
            RedirectToAction("Index", "_404") :
            View(__employeesData.Get());

        //// it must all the roles be specified! so, let's try some ways
        //[Authorize(Roles = "Staff, Administrators")]    // works, but it's not good to use explicit text!
        //[Authorize(Roles = Role.AdminOrStaff)]          // this way is better than the previous one
        [Authorize(Policy = Startup.AdminOrStaffPolicy)]  // or such way
        public ActionResult Emp_Details(int id)
        {
            if (__employeesData is null)
                return RedirectToAction("Index", "_404");
            else
                return (__employeesData.Get(id) is null) ? RedirectToAction("Index", "_404") : View(__employeesData.Get(id));
        }

        [Authorize(Roles = Role.Admin)]
        public ActionResult Emp_Edit(int id)
        {
            if (id == -1)
                return View(new EmployeeViewModel { Id = -1, Salary = 0.0F, Age = 0 });
            if (__employeesData is null)
                return RedirectToAction("Index", "_404");
            else
            {
                var emp = __employeesData.Get(id);
                return (emp is null) ? RedirectToAction("Index", "_404") : View(new EmployeeViewModel(emp));
            }
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public ActionResult Emp_Edit(EmployeeViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));
            if (model.FirstName == "Vasya" && model.Patronymic == "Vasilich" && model.LastName == "Pupkin")
                ModelState.AddModelError("", "sorry, it seems that this is fictional character! rename him, plz...");
            if (!ModelState.IsValid)
                return View(model);
            Employee emp = new Employee { Id = model.Id, Age = model.Age, Snils = model.Snils, Salary = model.Salary,
                LastName = model.LastName, FirstName = model.FirstName, Patronymic = model.Patronymic };
            if (model.Id == -1)
                __employeesData.Add(emp);
            else
                __employeesData.Update(emp);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Admin)]
        public ActionResult Emp_New() => RedirectToAction("Emp_Edit", new EmployeeViewModel() { Id = -1 });

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public ActionResult Emp_New(EmployeeViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));
            Employee emp = new Employee { Id = model.Id, Age = model.Age, Snils = model.Snils, Salary = model.Salary,
                LastName = model.LastName, FirstName = model.FirstName, Patronymic = model.Patronymic };
            __employeesData.Add(emp);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Admin)]
        public ActionResult Emp_Del(int id)
        {
            if (__employeesData is null)
                return BadRequest();
            var emp = __employeesData.Get(id);
            return (emp is null) ? BadRequest() : View(new EmployeeViewModel(emp));
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public ActionResult Emp_Del_BadGuy(int id)
        {
            if (__employeesData is null)
                return BadRequest();
            __employeesData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
