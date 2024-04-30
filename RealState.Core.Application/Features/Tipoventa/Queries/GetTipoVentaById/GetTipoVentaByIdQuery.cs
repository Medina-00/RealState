

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.Tipoventa;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Tipoventa.Queries.GetTipoVentaById
{
    public class GetTipoVentaByIdQuery : IRequest<Response<TipoVentaResponse>>
    {
        public int Id { get; set; }
    }
    public class GetTipoVentaByIdQueryHandle : IRequestHandler<GetTipoVentaByIdQuery, Response<TipoVentaResponse>>
    {
        private readonly ITipoVentaRepository tipoVentaRepository;
        private readonly IMapper mapper;

        public GetTipoVentaByIdQueryHandle(ITipoVentaRepository tipoVentaRepository,IMapper mapper)
        {
            this.tipoVentaRepository = tipoVentaRepository;
            this.mapper = mapper;
        }
        public async Task<Response<TipoVentaResponse>> Handle(GetTipoVentaByIdQuery request, CancellationToken cancellationToken)
        {
            var Result = await GetViewModelById(request);

            if (Result == null) throw new ApiExteption("No encontro Tipo de Venta", (int)HttpStatusCode.NotFound);
            return new Response<TipoVentaResponse>( Result);
        }

        private async Task<TipoVentaResponse> GetViewModelById(GetTipoVentaByIdQuery request)
        {
            var entityList = await tipoVentaRepository.GetByIdAsync(request.Id);

            return mapper.Map<TipoVentaResponse>(entityList);
        }
    }

}
