using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GbWebApp.Domain.Entities.Base;

namespace GbWebApp.Domain.Entities.MailSender
{
    public class Server : NamedEntity
    {
        [Required]
        public string Address { get; set; }

        public int Port { get; set; } = 25;

        public bool UseSSL { get; set; }

        [NotMapped]
        public string DisplayName => $"{Name} ({Address})";
    }
}
