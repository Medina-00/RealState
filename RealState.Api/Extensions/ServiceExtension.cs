using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace RealState.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RealState API",
                    Description = "This Api will be responsible for overall data distribution",
                    Contact = new OpenApiContact
                    {
                        Name = "Luis Angel Morel",
                        Email = "20221063.edu.do"
                    }
                });

                //esto es para que todos los paremetros tengas con el formato de camello.
                options.DescribeAllParametersInCamelCase();
                options.EnableAnnotations();
                //Aqui configuramos el APi Para que nos permitas Los Token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Ingrese Su token En el siguiente Formato - Bearer {Tu Token Aqui!!}"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });

            });
        }

        //esto es para gestionar las versiones del API
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                //aqui configuramos la version inicial del API.
                config.DefaultApiVersion = new ApiVersion(1, 0);
                //esto es para que si no especifican la version a correr , que se corra la version por defecto.
                config.AssumeDefaultVersionWhenUnspecified = true;
                //esto es para indicar que hay mas de una version de un EndPoint.
                config.ReportApiVersions = true;
            });
        }

    }
}
