

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropiedadMejoraRepository : GenericRepository<PropiedadMejora> ,IPropiedadMejoraRepository
    {
        public PropiedadMejoraRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
