using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbWebApp.Domain.Entities
{
    public class Employee : Entity
    {
        [Required]
        public string LastName { get; set; } = "Pupkin";

        [Required]
        public string FirstName { get; set; } = "Vasya";

        public string Patronymic { get; set; } = "Vasilich";

        [Required]
        public string Snils { get; set; } = "070-000-000";

        public float Salary { get; set; } = 100_000;

        public int Age { get; set; } = 33;

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        //public Employee() { }
    }
}
