using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;

namespace RealState.Core.Application.Features.Propiedad.Queries.GetPropiedadesByUser
{
    public class GetPropiedadesByUserQuery : IRequest<Response<ICollection<PropiedadReponse>>>
    {
        public string UserId { get; set; }
    }

    public class GetPropiedadesByUserQueryHandle : IRequestHandler<GetPropiedadesByUserQuery, Response<ICollection<PropiedadReponse>>>
    {
        private readonly IMapper mapper;
        private readonly IPropiedadRepository propiedadRepository;

        public GetPropiedadesByUserQueryHandle(IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mapper = mapper;
            this.propiedadRepository = propiedadRepository;
        }
        public async Task<Response<ICollection<PropiedadReponse>>> Handle(GetPropiedadesByUserQuery request, CancellationToken cancellationToken)
        {
            var propiedadList = await GetAllViewModelByUser(request);



            if (propiedadList == null) throw new ApiExteption("No encontro propiedades", (int)HttpStatusCode.NotFound);
            return new Response<ICollection<PropiedadReponse>>(propiedadList);
        }


        private async Task<List<PropiedadReponse>> GetAllViewModelByUser(GetPropiedadesByUserQuery query)
        {
            var entityList = await propiedadRepository.GetAllAsync();
            var data = entityList.Where(p => p.UserId == query.UserId);

            return mapper.Map<List<PropiedadReponse>>(data);
        }
    }
}
