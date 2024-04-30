

using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.PropiedadFavorita;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class PropiedadFavoritaService : GenericService<SavePropiedadFavoritaViewModel,PropiedadFavoritaViewModel ,PropiedadFavorita> ,IPropiedadFavoritaService
    {
        private readonly IPropiedadFavoritaRepository propiedadFavoritaRepository;
        private readonly IMapper mapper;

        public PropiedadFavoritaService( IPropiedadFavoritaRepository propiedadFavoritaRepository , IMapper mapper) : base(propiedadFavoritaRepository,mapper)
        {
            this.propiedadFavoritaRepository = propiedadFavoritaRepository;
            this.mapper = mapper;
        }
        public override async Task<SavePropiedadFavoritaViewModel> Add(SavePropiedadFavoritaViewModel vm)
        {
            PropiedadFavorita entity = mapper.Map<PropiedadFavorita>(vm);
            var favoritas = await propiedadFavoritaRepository.GetAllAsync();
            if(favoritas.Any(f => f.IdPropiedad == vm.IdPropiedad && f.UserId == vm.UserId))
            {
                throw new InvalidOperationException("No se puede borrar la esta propiedad, ya la tienes agregada.");

            }
            entity = await propiedadFavoritaRepository.AddAsync(entity);

            SavePropiedadFavoritaViewModel entityVm = mapper.Map<SavePropiedadFavoritaViewModel>(entity);

            return entityVm;
        }
    }
}
