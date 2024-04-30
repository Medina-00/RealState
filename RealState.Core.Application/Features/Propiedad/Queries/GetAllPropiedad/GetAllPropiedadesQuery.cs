

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
    public class GetAllPropiedadesQuery : IRequest<Response<ICollection<PropiedadReponse>>>
    {
       

    }

    public class GetAllPropiedadesQueryHandler : IRequestHandler<GetAllPropiedadesQuery, Response<ICollection<PropiedadReponse>>>
    {
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IMapper mapper;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;
        private readonly IMejoraRepository mejoraRepository;

        public GetAllPropiedadesQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper ,  IPropiedadMejoraRepository propiedadMejoraRepository, IMejoraRepository mejoraRepository)
        {
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.mejoraRepository = mejoraRepository;
        }

        public async Task<Response<ICollection<PropiedadReponse>>> Handle(GetAllPropiedadesQuery query, CancellationToken cancellationToken)
        {
            var propiedadList = await GetAllViewModel();
            //var PropiedadMejora = await propiedadMejoraRepository.GetAllAsync();
            //var mejoras = await mejoraRepository.GetAllAsync();
            //foreach (var item in propiedadList)
            //{
            //    var propiedadesMejorasItem = PropiedadMejora.Where(pm => pm.IdPropiedad == item.IdPropiedad);
            //    foreach (var propiedadMejora in propiedadesMejorasItem)
            //    {
            //        var mejora = mejoras.Find(m => m.Id == propiedadMejora.IdMejora);
            //        if (mejora != null)
            //        {
            //            if (item.Mejoras == null)
            //            {
            //                item.Mejoras = new List<Mejora>(); 
            //            }
            //            item.Mejoras.Add(mejora);
            //        }
            //    }
            //}

            if (propiedadList == null || propiedadList.Count == 0) throw new ApiExteption("No encontro propiedades", (int)HttpStatusCode.NotFound);
            return new Response<ICollection<PropiedadReponse>>(propiedadList);
        }


        private  async Task<List<PropiedadReponse>> GetAllViewModel()
        {
            var entityList = await propiedadRepository.GetAllAsync();
            
            return mapper.Map<List<PropiedadReponse>>(entityList);
        }
    }
}
