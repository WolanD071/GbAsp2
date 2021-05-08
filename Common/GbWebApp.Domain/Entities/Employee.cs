using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbWebApp.Domain.Entities
{
    /// <summary> employee model </summary>
    public class Employee : Entity
    {
        /// <summary> last name </summary>
        [Required]
        public string LastName { get; set; } = "Pupkin";

        /// <summary> first name </summary>
        [Required]
        public string FirstName { get; set; } = "Vasya";

        /// <summary> patronymic (optional) </summary>
        public string Patronymic { get; set; } = "Vasilich";

        /// <summary> SNILS number </summary>
        [Required]
        public string Snils { get; set; } = "070-000-000";

        /// <summary> employee's reward </summary>
        public float Salary { get; set; } = 100_000;

        /// <summary> age </summary>
        public int Age { get; set; } = 33;

        /// <summary> foreign key - database user this employee associated with </summary>
        public string UserId { get; set; }

        /// <summary> foreign key - database user this employee associated with </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
