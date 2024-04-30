

using AutoMapper;
using MediatR;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Features.Tipopropiedad.Commands.CreateTipoPropiedad
{
    public class CreateTipoPropiedadCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }

    public class CreateTipoPropiedadCommandHandle : IRequestHandler<CreateTipoPropiedadCommand, Response<int>>
    {
        private readonly ITipoPropiedadRepository tipoPropiedadRepository;
        private readonly IMapper mapper;

        public CreateTipoPropiedadCommandHandle(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            this.tipoPropiedadRepository = tipoPropiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<TipoPropiedad>(request);
            await tipoPropiedadRepository.AddAsync(result);
            return new Response<int>(result.Id);
        }



    }
}
