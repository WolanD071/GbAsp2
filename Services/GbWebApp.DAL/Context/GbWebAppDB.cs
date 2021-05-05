using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.Entities.Identity;

namespace GbWebApp.DAL.Context
{
    public class GbWebAppDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Order> Orders { get; set; }

        public GbWebAppDB(DbContextOptions<GbWebAppDB> options) : base(options) { }
    }
}
