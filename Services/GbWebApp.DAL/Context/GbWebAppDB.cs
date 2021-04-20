using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Domain.Entities.MailSender;

namespace GbWebApp.DAL.Context
{
    public class GbWebAppDB : IdentityDbContext<User, Role, string>
    {
        #region GbWebApp
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        #endregion

        #region GbWpfApp
        public DbSet<Server> Servers { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Letter> Letters { get; set; }
        #endregion

        public GbWebAppDB(DbContextOptions<GbWebAppDB> options) : base(options) { }
    }
}
