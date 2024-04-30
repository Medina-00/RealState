
using Microsoft.AspNetCore.Identity;
using RealState.Core.Application.Enums;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Seeds
{
    public static class UserAdmin
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser Admin = new();

            Admin.Nombre = "Angel";
            Admin.Apellido = "Luis";
            Admin.UserName = "Admin01";
            Admin.Email = "Admin01@gamil.com";
            Admin.EmailConfirmed = true;
            Admin.PhoneNumber = "8098765434";
            Admin.PhoneNumberConfirmed = true;
            Admin.Activo = true;
            

            if (userManager.Users.All(u => u.Id != Admin.Id))
            {
                var user = await userManager.FindByEmailAsync(Admin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(Admin, "HolaMundo12*");
                    await userManager.AddToRoleAsync(Admin, Roles.Administrador.ToString());
                }
            }



        }

    }
}
