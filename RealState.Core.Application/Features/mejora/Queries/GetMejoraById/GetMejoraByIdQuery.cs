

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.mejora.Queries.GetMejoraById
{
    public class GetMejoraByIdQuery : IRequest<Response<MejoraResponse>>
    {
        public int Id { get; set; }
    }

    public class GetMejoraByIdQueryHandle : IRequestHandler<GetMejoraByIdQuery, Response<MejoraResponse>>
    {
        private readonly IMejoraRepository mejoraRepository;
        private readonly IMapper mapper;

        public GetMejoraByIdQueryHandle(IMejoraRepository mejoraRepository, IMapper mapper)
        {
            this.mejoraRepository = mejoraRepository;
            this.mapper = mapper;
        }
        public async Task<Response<MejoraResponse>> Handle(GetMejoraByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await GetViewModelById(request);

            if (result == null) throw new ApiExteption("No encontro Mejoras", (int)HttpStatusCode.NotFound);
            return new Response<MejoraResponse>(result);
        }

        private async Task<MejoraResponse> GetViewModelById(GetMejoraByIdQuery request)
        {
            var entityList = await mejoraRepository.GetByIdAsync(request.Id);

            return mapper.Map<MejoraResponse>(entityList);
        }
    }


}
