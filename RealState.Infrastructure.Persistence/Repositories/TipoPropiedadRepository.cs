

using Microsoft.EntityFrameworkCore;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;
using System;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class TipoPropiedadRepository : GenericRepository<TipoPropiedad> , ITipoPropiedadRepository
    {
        private readonly ApplicationContext applicationContext;

        public TipoPropiedadRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public override async Task<List<TipoPropiedad>> GetAllAsync()
        {
            var tipoPropiedades = await applicationContext.Set<TipoPropiedad>().ToListAsync();

            foreach (var tipoPropiedad in tipoPropiedades)
            {
                // Obtener la cantidad de propiedades asociadas a este tipo de propiedad
                var cantidadPropiedades =  applicationContext.Set<Propiedad>().Where(p => p.TipoPropiedadId == tipoPropiedad.Id).Count();

                // Asignar la cantidad al tipo de propiedad
                tipoPropiedad.Cantidad = cantidadPropiedades;
            }

            return tipoPropiedades;
        }

    }
}
