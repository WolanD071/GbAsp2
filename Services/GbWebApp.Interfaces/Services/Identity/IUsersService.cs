using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace GbWebApp.Interfaces.Services.Identity
{
    public interface IUsersService :
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>,
        IUserLoginStore<User>,
        IUserClaimStore<User> { }
}
