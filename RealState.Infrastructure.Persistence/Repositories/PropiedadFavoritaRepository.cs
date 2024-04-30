

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;
using System;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropiedadFavoritaRepository : GenericRepository<PropiedadFavorita>, IPropiedadFavoritaRepository
    {
        private readonly ApplicationContext applicationContext;

        public PropiedadFavoritaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            
        }

       

    }
}
