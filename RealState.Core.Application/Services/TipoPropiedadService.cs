

using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.TipoPropiedad;
using RealState.Core.Application.ViewModel.TipoVenta;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class TipoPropiedadService: GenericService<SaveTipoPropiedadViewModel , TipoPropiedadViewModel, TipoPropiedad>, ITipoPropiedadService
    {
        private readonly ITipoPropiedadRepository tipoPropiedadRepository;
        private readonly IMapper mapper;
        private readonly IPropiedadRepository propiedadRepository;

        public TipoPropiedadService(ITipoPropiedadRepository tipoPropiedadRepository,IMapper mapper,IPropiedadRepository propiedadRepository) : base(tipoPropiedadRepository, mapper)
        {
            this.tipoPropiedadRepository = tipoPropiedadRepository;
            this.mapper = mapper;
            this.propiedadRepository = propiedadRepository;
        }

        public override async Task Update(SaveTipoPropiedadViewModel vm, int id)
        {
            
            TipoPropiedad entity = mapper.Map<TipoPropiedad>(vm);
            entity.Id = vm.Id;
            await tipoPropiedadRepository.UpdateAsync(entity, id);
        }
       
    }
    
}
