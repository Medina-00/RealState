

using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.TipoVenta;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class TipoVentaService : GenericService<SaveTipoVentaViewModel , TipoVentaViewModel, TipoVenta>, ITipoVentaService
    {
        private readonly ITipoVentaRepository tipoVentaRepository;
        private readonly IMapper mapper;
        private readonly IPropiedadRepository propiedadRepository;

        public TipoVentaService(ITipoVentaRepository tipoVentaRepository,IMapper mapper, IPropiedadRepository propiedadRepository) : base(tipoVentaRepository,mapper)
        {
            this.tipoVentaRepository = tipoVentaRepository;
            this.mapper = mapper;
            this.propiedadRepository = propiedadRepository;
        }

        public override async Task<List<TipoVentaViewModel>> GetAllViewModel()
        {
            var entityList = await tipoVentaRepository.GetAllAsync();
            int cantidad = 0;
            foreach (var entity in entityList)
            {
                var propiedades = await propiedadRepository.GetAllAsync();
                cantidad = propiedades.Count(p => p.TipoVentaId == entity.Id);
                entity.Cantidad = cantidad;
            }



            return mapper.Map<List<TipoVentaViewModel>>(entityList);
        }
    }
}
