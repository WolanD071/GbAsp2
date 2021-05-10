using System.IO;
using System.Linq;
using GbWebApp.Logger;
using GbWebApp.DAL.Context;
using GbWebApp.Services.Data;
using Microsoft.OpenApi.Models;
using GbWebApp.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GbWebApp.Services.Services.InDB;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GbWebApp.ServiceHosting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

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

            services.AddTransient(typeof(IAnyEntityCRUD<>), typeof(InDbAnyEntity<>));
            services.AddTransient<IProductService, InDbProductData>();
            services.AddTransient<IOrderService, InDbOrderData>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "GbWebApp.ServiceHosting", Version = "v1"});
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                const string hosting_xml = "GbWebApp.ServiceHosting.xml";
                const string domain_xml = "GbWebApp.Domain.xml";
                const string debug_path = "bin/debug/net5.0";

                if (File.Exists(hosting_xml))
                    c.IncludeXmlComments(hosting_xml);
                else if (File.Exists(Path.Combine(debug_path, hosting_xml)))
                    c.IncludeXmlComments(Path.Combine(debug_path, hosting_xml));

                if (File.Exists(domain_xml))
                    c.IncludeXmlComments(domain_xml);
                else if (File.Exists(Path.Combine(debug_path, domain_xml)))
                    c.IncludeXmlComments(Path.Combine(debug_path, domain_xml));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDBInitializer db, ILoggerFactory logger)
        {
            logger.AddLog4Net();
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GbWebApp.ServiceHosting v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
