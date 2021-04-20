using Microsoft.AspNetCore.Identity;

namespace GbWebApp.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string Admin = "Administrators";
        public const string Users = "Users";
        public const string Staff = "Staff";
        public const string AdminOrStaff = "Administrators, Staff";
        public string Description { get; set; }
    }
}