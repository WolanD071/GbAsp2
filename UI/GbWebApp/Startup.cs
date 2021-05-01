using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GbWebApp.DAL.Context;
using GbWebApp.Interfaces.Services;
using GbWebApp.Services.Data;
using GbWebApp.Services.Services.InCookies;
using GbWebApp.Services.Services.InDB;

namespace GbWebApp
{
    public class Startup
    {
        public const string AdminOrStaffPolicy = "AdminOrStaff";
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GbWebAppDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddTransient<AppDBInitializer>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<GbWebAppDB>()
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

            //services.AddTransient<IAnyEntityCRUD<IEntity>, InDbAnyEntity<IEntity>>();     // doesn't work
            //services.AddTransient<IAnyEntityCRUD<BlogPost>, InDbAnyEntity<BlogPost>>();   // works, but it's only special case...
            services.AddTransient(typeof(IAnyEntityCRUD<>), typeof(InDbAnyEntity<>));       // works, and this is the general case!
            services.AddTransient<IProductData, InDbProductData>();
            services.AddTransient<ICartService, InCookiesCartService>();
            services.AddTransient<IOrderService, InDbOrdertData>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization(opt =>
            { opt.AddPolicy(AdminOrStaffPolicy, policy => policy.RequireRole(Role.Admin, Role.Staff)); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDBInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("pagination", "Shop/Page{prodPage}", new { Controller = "Shop", action = "Index" });
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
