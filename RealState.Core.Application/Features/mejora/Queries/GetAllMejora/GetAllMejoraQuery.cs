

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.mejora.Queries.GetAllMejora
{
    public class GetAllMejoraQuery : IRequest<Response<ICollection<MejoraResponse>>>
    {

    }

    public class GetAllMejoraQueryHandle : IRequestHandler<GetAllMejoraQuery, Response<ICollection<MejoraResponse>>>
    {
        private readonly IMapper mapper;
        private readonly IMejoraRepository mejoraRepository;

        public GetAllMejoraQueryHandle(IMapper mapper, IMejoraRepository mejoraRepository)
        {
            this.mapper = mapper;
            this.mejoraRepository = mejoraRepository;
        }
        public async Task<Response<ICollection<MejoraResponse>>> Handle(GetAllMejoraQuery request, CancellationToken cancellationToken)
        {
            var result = await GetAllViewModel();

            if (result == null || result.Count == 0) throw new ApiExteption("No encontro Mejoras", (int)HttpStatusCode.NotFound);
            return new Response<ICollection<MejoraResponse>>(result);
        }

        private async Task<ICollection<MejoraResponse>> GetAllViewModel()
        {
            var entityList = await mejoraRepository.GetAllAsync();

            return mapper.Map<ICollection<MejoraResponse>>(entityList);
        }
    }


}
