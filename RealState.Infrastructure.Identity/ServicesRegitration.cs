using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealState.Core.Application.Interfaces;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Settings;
using RealState.Infrastructure.Identity.Context;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Service;
using System.Text;

namespace RealState.Infrastructure.Identity
{
    public static class ServicesRegitration
    {
        public static void AddIdentityInfrastructureApi(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            ContextConfigartion(services, configuration);
            #endregion

            #region Edentity
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            //ESTO ES PARA CUANDO NO TENGA ACCESO A ALGO INTENTADO ENTRAR POR LA URL , TE MANDE AL LOGIN
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/User/AccessDenied";

            });

            #endregion


            #region JWT

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                //Esto es para que el token se guarde.
                options.SaveToken = false;
                //Esto es para indicar Cuales Datos Debe traer un token para aceptarlo en la App.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Validar el token del token que esta activo o en seccion
                    ValidateIssuerSigningKey = true,
                    //Validamos El Emisor del Token 
                    ValidateIssuer = true,
                    //Validamos La Audiencia del Token 
                    ValidateAudience = true,
                    //Validamos La Vida del Token , mejor dicho si no ha expirado
                    ValidateLifetime = true,
                    //Esto es para indicar que si ya vencio el token , que no tenga vida.
                    ClockSkew = TimeSpan.Zero,
                    //Aqui traemos el emisor de los settings
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    //Aqui traemos el Audiencia de los settings
                    ValidAudience = configuration["JWTSettings:Audience"],
                    //Aqui traemos La llave de los settings
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!))
                };
                //Esto es para Configurar los eventos dependiendo del resultado del token.
                options.Events = new JwtBearerEvents()
                {
                    //Esto es por si la Authenticacion Falla.
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    //Esto es esta intentando entrar a una seccion , pero el token no esta Authorizado y tampoco es Valido.
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        //aqui convierto en Json la respuesta.
                        var result = JsonConvert.SerializeObject(new Response<string>("El Token No es Valido Y No Estas Authorizado!!"));
                        return c.Response.WriteAsync(result);
                    },
                    //Esto es esta intentando entrar a una seccion , pero el token no esta Authorizado.
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no está autorizada para acceder a esta Seccion"));
                        return c.Response.WriteAsync(result);
                    }
                };

            });

            #endregion

            #region Services
            ServiceConfigartion(services);

            #endregion

        }


        public static void AddIdentityInfrastructureForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            ContextConfigartion(services, configuration);
            #endregion


            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            //ESTO ES PARA CUANDO NO TENGA ACCESO A ALGO INTENTADO ENTRAR POR LA URL , TE MANDE AL LOGIN
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/User/AccessDenied";

            });
            services.AddAuthentication();
            #endregion

            #region Services
            ServiceConfigartion(services);

            #endregion
        }



        #region Private Methods


        private static void ContextConfigartion(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<IdentityContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                     m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            
        }


        private static void ServiceConfigartion(IServiceCollection services)
        {
            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

        #endregion
    }
}

