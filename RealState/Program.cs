using Microsoft.AspNetCore.Identity;
using RealState.Infrastructure.Identity;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Seeds;
using RealState.Infrastructure.Shared;
using RealState.Core.Application;
using RealState.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddIdentityInfrastructureForWeb(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedsAsync(userManager, roleManager);
        await UserAdmin.SeedsAsync(userManager, roleManager);
        await UserAgente.SeedsAsync(userManager, roleManager);
        await UserCliente.SeedsAsync(userManager, roleManager);

    }
    catch (Exception ex) { Console.WriteLine(ex); }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Propiedad}/{action=Index}/{id?}");

app.Run();
