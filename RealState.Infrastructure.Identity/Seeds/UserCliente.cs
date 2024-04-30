

using Microsoft.AspNetCore.Identity;
using RealState.Core.Application.Enums;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Seeds
{
    public static class UserCliente
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser Cliente = new();

            Cliente.Nombre = "Luis";
            Cliente.Apellido = "Angel";
            Cliente.UserName = "Cliente01";
            Cliente.Email = "Cliente01@gamil.com";
            Cliente.EmailConfirmed = true;
            Cliente.PhoneNumber = "8098765433";
            Cliente.PhoneNumberConfirmed = true;
            Cliente.Activo = true;


            if (userManager.Users.All(u => u.Id != Cliente.Id))
            {
                var user = await userManager.FindByEmailAsync(Cliente.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(Cliente, "HolaMundo12*");
                    await userManager.AddToRoleAsync(Cliente, Roles.Cliente.ToString());
                }
            }



        }
    }
}
