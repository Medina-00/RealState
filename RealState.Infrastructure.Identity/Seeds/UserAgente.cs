using Microsoft.AspNetCore.Identity;
using RealState.Core.Application.Enums;
using RealState.Infrastructure.Identity.Entities;


namespace RealState.Infrastructure.Identity.Seeds
{
    public static class UserAgente
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser Admin = new();

            Admin.Nombre = "Felix";
            Admin.Apellido = "Morel";
            Admin.UserName = "Agente01";
            Admin.Email = "Agente01@gamil.com";
            Admin.EmailConfirmed = true;
            Admin.PhoneNumber = "8098765435";
            Admin.PhoneNumberConfirmed = true;
            Admin.Activo = true;
            Admin.Foto = "https://th.bing.com/th/id/OIP.KepY6B4vlRy1x0yhU4-MsgHaHb?rs=1&pid=ImgDetMain";


            if (userManager.Users.All(u => u.Id != Admin.Id))
            {
                var user = await userManager.FindByEmailAsync(Admin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(Admin, "HolaMundo12*");
                    await userManager.AddToRoleAsync(Admin, Roles.Agente.ToString());
                }
            }



        }
    }
}
