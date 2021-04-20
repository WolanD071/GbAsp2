using Microsoft.AspNetCore.Identity;

namespace GbWebApp.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Admin";

        public const string DefaultAdminPassword = "Admin";

        public string Description { get; set; }
    }
}
