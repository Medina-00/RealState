

using AutoMapper;
using MediatR;
using RealState.Core.Application.Features.Tipopropiedad.Commands.CreateTipoPropiedad;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Features.Tipoventa.Commands.CreateTipoVenta
{
    public class CreateTipoVentaCommand :IRequest<Response<int>>
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }

    public class CreateTipoVentaCommandHandle : IRequestHandler<CreateTipoVentaCommand, Response<int>>
    {
        private readonly ITipoVentaRepository tipoVentaRepository;
        private readonly IMapper mapper;

        public CreateTipoVentaCommandHandle(ITipoVentaRepository tipoVentaRepository,IMapper mapper)
        {
            this.tipoVentaRepository = tipoVentaRepository;
            this.mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateTipoVentaCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<TipoVenta>(request);
            await tipoVentaRepository.AddAsync(result);
            return new Response<int>( result.Id);
        }



    }
}
