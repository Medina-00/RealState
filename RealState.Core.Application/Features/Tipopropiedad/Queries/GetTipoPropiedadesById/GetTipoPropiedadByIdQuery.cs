

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Tipopropiedad.Queries.GetTipoPropiedadesById
{
    public class GetTipoPropiedadByIdQuery : IRequest<Response<TipoPropiedadResponse>>
    {
        public int Id { get; set; }
    }

    public class GetTipoPropiedadByIdQueryHandler : IRequestHandler<GetTipoPropiedadByIdQuery, Response<TipoPropiedadResponse>>
    {
        private readonly ITipoPropiedadRepository tipoPropiedadRepository;
        private readonly IMapper mapper;

        public GetTipoPropiedadByIdQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            this.tipoPropiedadRepository = tipoPropiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<TipoPropiedadResponse>> Handle(GetTipoPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var TipoPropiedad = await GetViewModelById(request);

            if (TipoPropiedad == null) throw new ApiExteption("No encontro Tipo de Propiedad", (int)HttpStatusCode.NotFound);

            return new Response<TipoPropiedadResponse>(TipoPropiedad);
        }

        private async Task<TipoPropiedadResponse> GetViewModelById(GetTipoPropiedadByIdQuery request)
        {
            var entityList = await tipoPropiedadRepository.GetByIdAsync(request.Id);

            return mapper.Map<TipoPropiedadResponse>(entityList);
        }
    }
}
