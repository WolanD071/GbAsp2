using GbWebApp.Domain.Entities.Base;

namespace GbWebApp.Domain.Entities.MailSender
{
    public class Letter : Entity
    {
        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
