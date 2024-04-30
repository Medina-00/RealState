

using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.Mejora;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class MejoraService : GenericService<SaveMejoraViewModel,MejoraViewModel ,Mejora> ,IMejoraService
    {
        public MejoraService(IMejoraRepository mejoraRepository , IMapper mapper) : base(mejoraRepository,mapper)
        {
            
        }
    }
}
