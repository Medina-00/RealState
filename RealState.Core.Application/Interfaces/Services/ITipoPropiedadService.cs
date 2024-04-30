

using RealState.Core.Application.ViewModel.TipoPropiedad;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface ITipoPropiedadService : IGenericService<SaveTipoPropiedadViewModel , TipoPropiedadViewModel ,TipoPropiedad>
    {
    }
}
