

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Dtos.Tipoventa;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Tipoventa.Queries.GetAllTipoVenta
{
    public class GetAllTipoVentaQuery : IRequest<Response<ICollection<TipoVentaResponse>>>
    {
    }

    public class GetAllTipoVentaQueryHandle : IRequestHandler<GetAllTipoVentaQuery, Response<ICollection<TipoVentaResponse>>>
    {
        private readonly ITipoVentaRepository tipoVentaRepository;
        private readonly IMapper mapper;

        public GetAllTipoVentaQueryHandle(ITipoVentaRepository tipoVentaRepository , IMapper mapper)
        {
            this.tipoVentaRepository = tipoVentaRepository;
            this.mapper = mapper;
        }
        public async Task<Response<ICollection<TipoVentaResponse>>> Handle(GetAllTipoVentaQuery request, CancellationToken cancellationToken)
        {
            var Result = await GetAllViewModel();

            if (Result == null || Result.Count == 0) throw new ApiExteption("No encontro Tipos de Ventas", (int)HttpStatusCode.NotFound);
            return new Response<ICollection<TipoVentaResponse>>( Result);
        }


        private async Task<List<TipoVentaResponse>> GetAllViewModel()
        {
            var entityList = await tipoVentaRepository.GetAllAsync();

            return mapper.Map<List<TipoVentaResponse>>(entityList);
        }
    }

    
}
