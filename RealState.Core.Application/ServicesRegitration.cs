using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using System.Reflection;

namespace RealState.Core.Application
{
    public static class ServicesRegitration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Services

            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMejoraService, MejoraService>();
            services.AddTransient<ITipoPropiedadService, TipoPropiedadService>();
            services.AddTransient<ITipoVentaService, TipoVentaService>();
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient<IPropiedadMejoraService, PropiedadMejoraService>();
            services.AddTransient<IPropiedadFavoritaService, PropiedadFavoritaService>();
            //services.AddTransient<IAvenceEfectivoService, AvanceEfectivoService>();
            //services.AddTransient<ITransferenciaService, TransferenciaService>();


            #endregion


        }

    }
}
