using Microsoft.Extensions.DependencyInjection;
using GbWebApp.Services.Services.InCookies;
using GbWebApp.Infrastructure.Middleware;
using Microsoft.Extensions.Configuration;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Logging;
using GbWebApp.Interfaces.TestAPI;
using GbWebApp.Services.Services;
using GbWebApp.Clients.Employees;
using GbWebApp.Clients.BlogPosts;
using GbWebApp.Clients.Identity;
using GbWebApp.Clients.Products;
using GbWebApp.Domain.Entities;
using GbWebApp.Clients.Orders;
using GbWebApp.Clients.Values;
using GbWebApp.Logger;

namespace GbWebApp
{
    public class Startup
    {
        public const string AdminOrStaffPolicy = "AdminOrStaff";
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddIdentityAPIClients()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(5);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "GbWebAppCook";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = System.TimeSpan.FromDays(10);
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";
                opt.SlidingExpiration = true;
            });

            services.AddTransient<IAnyEntityCRUD<Employee>, EmployeesClient>();
            services.AddTransient<IAnyEntityCRUD<BlogPost>, BlogPostsClient>();
            services.AddTransient<ICartCookie, InCookiesCartCookie>();
            services.AddTransient<IProductService, ProductsClient>();
            services.AddTransient<IValuesService, ValuesClient>();
            services.AddTransient<IOrderService, OrdersClient>();
            services.AddTransient<ICartService, CartService>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization(opt =>
            { opt.AddPolicy(AdminOrStaffPolicy, policy => policy.RequireRole(Role.Admin, Role.Staff)); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            logger.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<MiddleErrorHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("pagination", "Shop/Page{prodPage}", new { Controller = "Shop", action = "Index" });
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
