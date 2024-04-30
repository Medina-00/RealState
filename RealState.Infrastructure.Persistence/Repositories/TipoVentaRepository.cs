

using Microsoft.EntityFrameworkCore;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class TipoVentaRepository : GenericRepository<TipoVenta> ,ITipoVentaRepository
    {
        private readonly ApplicationContext applicationContext;

        public TipoVentaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public override async Task<List<TipoVenta>> GetAllAsync()
        {
            var tipoVentas = await applicationContext.Set<TipoVenta>().ToListAsync();

            foreach (var tipoPropiedad in tipoVentas)
            {
                // Obtener la cantidad de propiedades asociadas a este tipo de propiedad
                var cantidadPropiedades = applicationContext.Set<Propiedad>().Where(p => p.TipoPropiedadId == tipoPropiedad.Id).Count();

                // Asignar la cantidad al tipo de propiedad
                tipoPropiedad.Cantidad = cantidadPropiedades;
            }

            return tipoVentas;
        }
    }
}
