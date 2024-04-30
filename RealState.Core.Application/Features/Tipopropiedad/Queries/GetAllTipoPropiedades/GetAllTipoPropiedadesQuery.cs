

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Features.Propiedad.Queries.GetAllPropiedad;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Tipopropiedad.Queries.GetAllTipoPropiedades
{
    public class GetAllTipoPropiedadesQuery : IRequest<Response<ICollection<TipoPropiedadResponse>>>
    { }

    public class GetAllTipoPropiedadesQueryHandler : IRequestHandler<GetAllTipoPropiedadesQuery, Response<ICollection<TipoPropiedadResponse>>>
    {
        private readonly ITipoPropiedadRepository tipoPropiedadRepository;
        private readonly IMapper mapper;

        public GetAllTipoPropiedadesQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            this.tipoPropiedadRepository = tipoPropiedadRepository;
            this.mapper = mapper;
        }



        public async Task<Response<ICollection<TipoPropiedadResponse>>> Handle(GetAllTipoPropiedadesQuery request, CancellationToken cancellationToken)
        {
            var TipoPropiedadList = await GetAllViewModel();

            if (TipoPropiedadList == null || TipoPropiedadList.Count == 0) throw new ApiExteption("No encontro Tipos de Propiedades", (int)HttpStatusCode.NotFound);
            return new Response<ICollection<TipoPropiedadResponse>>(TipoPropiedadList);
        }

        private async Task<ICollection<TipoPropiedadResponse>> GetAllViewModel()
        {
            var entityList = await tipoPropiedadRepository.GetAllAsync();

            return mapper.Map<ICollection<TipoPropiedadResponse>>(entityList);
        }
    }
}
