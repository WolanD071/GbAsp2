using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace GbWebApp.Interfaces.Services.Identity
{
    public interface IRolesService : IRoleStore<Role> { }
}
