

using Microsoft.AspNetCore.Identity;
using RealState.Core.Application.Enums;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Agente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Desarrollador.ToString()));

        }

    }
}
