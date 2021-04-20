using System.ComponentModel.DataAnnotations;
using GbWebApp.Domain.Entities.Base;

namespace GbWebApp.Domain.Entities.MailSender
{
    public class Recipient : NamedEntity
    {
        [Required]
        public string Email { get; set; }
    }
}
