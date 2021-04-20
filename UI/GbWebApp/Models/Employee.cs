using GbWebApp.ViewModels;

namespace GbWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; } = 1;

        public string LastName { get; set; } = "Pupkin";

        public string FirstName { get; set; } = "Vasya";

        public string Patronymic { get; set; } = "Vasilich";

        public string Snils { get; set; } = "070-000-000";

        public float Salary { get; set; } = 100_000;

        public int Age { get; set; } = 33;

        public Employee(EmployeeViewModel model)
        {
            Id = model.Id;
            Age = model.Age;
            Snils = model.Snils;
            Salary = model.Salary;
            LastName = model.LastName;
            FirstName = model.FirstName;
            Patronymic = model.Patronymic;
        }

        public Employee()
        {
        }
    }
}
