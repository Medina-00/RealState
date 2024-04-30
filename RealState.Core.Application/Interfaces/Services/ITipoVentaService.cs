

using RealState.Core.Application.ViewModel.TipoVenta;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface ITipoVentaService : IGenericService<SaveTipoVentaViewModel , TipoVentaViewModel , TipoVenta>
    {
    }
}
