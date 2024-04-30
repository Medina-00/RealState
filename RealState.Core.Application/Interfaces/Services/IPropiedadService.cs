

using RealState.Core.Application.ViewModel.Propiedad;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IPropiedadService : IGenericService<SavePropiedadViewModel, PropiedadViewModel , Propiedad>
    {
    }
}
