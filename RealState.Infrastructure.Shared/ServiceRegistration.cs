using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Domain.Settings;
using RealState.Infrastructure.Shared.Service;

namespace RealState.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //ESTO ES PARA LLENAR LA PROPIEDADES DE LOS SETTINGS DE DOMAIN
            services.Configure<MainSetting>(configuration.GetSection("MainSetting"));

            //AQUI HAGO LA INTECCIOON DE DEPENDENCIA
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}
