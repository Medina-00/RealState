

using RealState.Core.Application.ViewModel.PropiedadFavorita;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IPropiedadFavoritaService : IGenericService<SavePropiedadFavoritaViewModel , PropiedadFavoritaViewModel , PropiedadFavorita>
    {
    }
}
