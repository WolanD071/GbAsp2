using Microsoft.AspNetCore.Identity;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GbWebApp.Clients.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityAPIClients(this IServiceCollection services)
        {
            services
                .AddTransient<IUserStore<User>, UsersClient>()
                .AddTransient<IUserRoleStore<User>, UsersClient>()
                .AddTransient<IUserPasswordStore<User>, UsersClient>()
                .AddTransient<IUserEmailStore<User>, UsersClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTransient<IUserClaimStore<User>, UsersClient>()
                .AddTransient<IUserLoginStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            return services;
        }

        public static IdentityBuilder AddIdentityAPIClients(this IdentityBuilder builder)
        {
            builder.Services.AddIdentityAPIClients();
            return builder;
        }
    }
}
