

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;


namespace RealState.Core.Application.Features.Propiedad.Queries.GetByIdPropiedad
{
    public class GetPropiedadByIdQuery : IRequest<Response<PropiedadReponse>>
    {
        public int IdPropiedad { get; set; }
    }

    public class GetPropiedadByIdQueryHandler : IRequestHandler<GetPropiedadByIdQuery, Response<PropiedadReponse>>
    {
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IMapper mapper;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;
        private readonly IMejoraRepository mejoraRepository;

        public GetPropiedadByIdQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper ,IPropiedadMejoraRepository propiedadMejoraRepository ,IMejoraRepository mejoraRepository)
        {
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.mejoraRepository = mejoraRepository;
        }
        public async Task<Response<PropiedadReponse>> Handle(GetPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var propiedad = await GetViewModelById(request);
            //var PropiedadMejora = await propiedadMejoraRepository.GetAllAsync();
            //var mejoras = await mejoraRepository.GetAllAsync();
            //PropiedadMejora = PropiedadMejora.Where(pm => pm.IdPropiedad == propiedad.IdPropiedad).ToList();
            //foreach (var item in PropiedadMejora)
            //{
            //    var mejora = mejoras.Find(m => m.Id == item.IdPropiedad);
            //    propiedad.Mejoras.Add(mejora!);
            //}
            if (propiedad == null) throw new ApiExteption("No encontro la propiedad", (int)HttpStatusCode.NotFound);
            return new Response<PropiedadReponse>(propiedad)!;
        }


        private async Task<PropiedadReponse> GetViewModelById(GetPropiedadByIdQuery request)
        {
            var entity = await propiedadRepository.GetByIdAsync(request.IdPropiedad);            

            return mapper.Map<PropiedadReponse>(entity);
        }
    }

    



}
