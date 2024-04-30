

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;
using System.Net;


namespace RealState.Core.Application.Features.Propiedad.Queries.GetAllPropiedad
{
    public class GetPropiedadesByNumeroQuery : IRequest<Response<PropiedadReponse>>
    {
        public string Numero6Digito { get; set; }

    }

    public class GetPropiedadesByNumeroQueryHandler : IRequestHandler<GetPropiedadesByNumeroQuery, Response<PropiedadReponse>>
    {
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IMapper mapper;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;
        private readonly IMejoraRepository mejoraRepository;

        public GetPropiedadesByNumeroQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper ,  IPropiedadMejoraRepository propiedadMejoraRepository, IMejoraRepository mejoraRepository)
        {
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.mejoraRepository = mejoraRepository;
        }

        public async Task<Response<PropiedadReponse>> Handle(GetPropiedadesByNumeroQuery query, CancellationToken cancellationToken)
        {
            var propiedadList = await GetAllViewModel(query);
           


            if (propiedadList == null ) throw new ApiExteption("No encontro propiedades", (int)HttpStatusCode.NotFound);
            return new Response<PropiedadReponse>(propiedadList);
        }


        private  async Task<PropiedadReponse> GetAllViewModel(GetPropiedadesByNumeroQuery query)
        {
            var entityList = await propiedadRepository.GetAllAsync();
            var data = entityList.Find(p => p.Numero6Digitos == query.Numero6Digito);
            
            return mapper.Map<PropiedadReponse>(data);
        }
    }
}
