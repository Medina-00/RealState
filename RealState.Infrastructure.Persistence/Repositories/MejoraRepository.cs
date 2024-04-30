
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class MejoraRepository : GenericRepository<Mejora> ,IMejoraRepository
    {
        public MejoraRepository(ApplicationContext applicationContext) : base(applicationContext) { }
        
    }
}
