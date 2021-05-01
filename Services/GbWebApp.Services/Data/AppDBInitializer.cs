using System;
using System.Linq;
using System.Threading.Tasks;
using GbWebApp.DAL.Context;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Services.Data
{
    public class AppDBInitializer
    {
        readonly GbWebAppDB __db;
        readonly ILogger<AppDBInitializer> __logger;
        readonly UserManager<User> __userManager;
        readonly RoleManager<Role> __roleManager;

        public AppDBInitializer(GbWebAppDB db, ILogger<AppDBInitializer> logger,
            UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            __db = db;
            __logger = logger;
            __userManager = userManager;
            __roleManager = roleManager;
        }

        public void Initialize()
        {
            var db = __db.Database;

            __logger.LogInformation("DB initailizing ...");

            if (db.GetPendingMigrations().Any())
            {
                __logger.LogInformation("applying migration(s) ...");
                db.Migrate();
                __logger.LogInformation("applying migration(s) done!");
            }
            else
                __logger.LogInformation("DB is up to date, no migration required!");

            try
            {
                InitIdsAsync().Wait();
                InitEmployees();
                InitProducts();
            }
            catch (Exception e)
            {
                __logger.LogError($"an error was caused during DB init! {e.Message}");
                throw;
            }

            __logger.LogInformation("DB initailization complete!");
        }

        public async Task InitIdsAsync()
        {
            __logger.LogInformation("Identity system initializing...");


            async Task CheckRole(string RoleName)
            {
                if (!await __roleManager.RoleExistsAsync(RoleName))
                {
                    await __roleManager.CreateAsync(new Role { Name = RoleName });
                    __logger.LogInformation($"The role '{RoleName}' created successfully!");
                }
            }

            await CheckRole(Role.Admin);
            await CheckRole(Role.Staff);
            await CheckRole(Role.Users);

            if (await __userManager.FindByNameAsync(User.Administrator) is null)
            {
                __logger.LogWarning("Administrator account not found in DB!");
                var admin = new User { UserName = User.Administrator };

                var creation_result = await __userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    __logger.LogInformation("Administrator account created successfully!");
                    await __userManager.AddToRoleAsync(admin, Role.Admin);
                    __logger.LogInformation($"Administrator account has gained the '{Role.Admin}' role!");
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Error creating administrator account! Details: {string.Join(",", errors)}");
                }
            }

            __logger.LogInformation("Identity system initializing complete!");
        }

        public void InitEmployees()
        {
            var db = __db.Database;

            if (__db.Employees.Any())
            {
                __logger.LogInformation("no employees initailizing required ...");
                return;
            }

            __logger.LogInformation("employees initailizing ...");
            using (db.BeginTransaction())
            {
                __db.Employees.AddRange(TestData.Employees);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] ON");
                __db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] OFF");
                db.CommitTransaction();
            }
            __logger.LogInformation("employees initailization complete!");
        }

        public void InitProducts()
        {
            var db = __db.Database;

            if (__db.Products.Any())
            {
                __logger.LogInformation("no products initailizing required ...");
                return;
            }

            __logger.LogInformation("products initailizing ...");
            __logger.LogInformation("adding sections ...");
            using (db.BeginTransaction())
            {
                __db.Sections.AddRange(TestData.Sections);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                __db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                db.CommitTransaction();
            }
            __logger.LogInformation("sections added!");
            __logger.LogInformation("adding brands ...");
            using (db.BeginTransaction())
            {
                __db.Brands.AddRange(TestData.Brands);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                __db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                db.CommitTransaction();
            }
            __logger.LogInformation("brands added!");
            __logger.LogInformation("adding products ...");
            using (db.BeginTransaction())
            {
                __db.Products.AddRange(TestData.Products);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                __db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
                db.CommitTransaction();
            }
            __logger.LogInformation("products added!");
            __logger.LogInformation("products initailization complete!");
        }
    }
}
