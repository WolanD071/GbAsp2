using System.ComponentModel.DataAnnotations;
using GbWebApp.Domain.Entities.Base;

namespace GbWebApp.Domain.Entities.MailSender
{
    public class Sender : NamedEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
