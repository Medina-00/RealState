

using RealState.Core.Application.ViewModel.Mejora;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IMejoraService : IGenericService<SaveMejoraViewModel , MejoraViewModel , Mejora>
    {
    }
}
