

using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.Mejora;
using RealState.Core.Application.ViewModel.PropiedadMejora;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class PropiedadMejoraService : GenericService<SavePropiedadMejoraViewModel, PropiedadMejoraViewModel, PropiedadMejora>, IPropiedadMejoraService
    {
        public PropiedadMejoraService( IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper) : base(propiedadMejoraRepository, mapper)
        {

        }

    }
}
