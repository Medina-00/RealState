using RealState.Infrastructure.Identity;
using RealState.Infrastructure.Persistence;
using RealState.Core.Application;
using RealState.Api.Extensions;
using RealState.Infrastructure.Shared;
using Microsoft.AspNetCore.Identity;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( o =>
{
    o.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions( o =>
{
    o.SuppressInferBindingSourcesForParameters = true;
    o.SuppressMapClientErrors = true;
});



//--------------------------
builder.Services.AddIdentityInfrastructureApi(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);

//este servicio es para ver el estado de la API
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddApiVersioningExtension();
builder.Services.AddSwaggerExtension();
builder.Services.AddSession();
//------------------------------

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedsAsync(userManager, roleManager);
        await DefaultRoles.SeedsAsync(userManager, roleManager);
        await UserAdmin.SeedsAsync(userManager, roleManager);
        await UserAgente.SeedsAsync(userManager, roleManager);
        await UserCliente.SeedsAsync(userManager, roleManager);


    }
    catch (Exception ex) { Console.WriteLine(ex); }
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/healt");
app.UseSession();
app.MapControllers();

app.Run();
