using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Infrastructure.Persistence.Context;
using RealState.Infrastructure.Persistence.Repositories;

namespace RealState.Infrastructure.Persistence
{
    public static class ServiceRegitration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));


            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPropiedadRepository, PropiedadRepository>();
            services.AddTransient<ITipoVentaRepository, TipoVentaRepository>();
            services.AddTransient<ITipoPropiedadRepository, TipoPropiedadRepository>();
            services.AddTransient<IPropiedadMejoraRepository, PropiedadMejoraRepository>();
            services.AddTransient<IMejoraRepository, MejoraRepository>();
            services.AddTransient<IPropiedadFavoritaRepository, PropiedadFavoritaRepository>();

            #endregion
        }

    }
}
