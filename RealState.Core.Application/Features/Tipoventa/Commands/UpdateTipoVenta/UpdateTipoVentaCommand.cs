

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Dtos.Tipoventa;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Features.Tipopropiedad.Commands.UpdateTipoPropiedad;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;
using System.Net;

namespace RealState.Core.Application.Features.Tipoventa.Commands.UpdateTipoVenta
{
    public class UpdateTipoVentaCommand : IRequest<Response<TipoVentaResponse>>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateTipoVentaCommandHandle : IRequestHandler<UpdateTipoVentaCommand, Response<TipoVentaResponse>>
    {

        private readonly IMapper mapper;
        private readonly ITipoVentaRepository tipoVentaRepository;

        public UpdateTipoVentaCommandHandle(IMapper mapper, ITipoVentaRepository tipoVentaRepository)
        {

            this.mapper = mapper;
            this.tipoVentaRepository = tipoVentaRepository;
        }
        public async Task<Response<TipoVentaResponse>> Handle(UpdateTipoVentaCommand request, CancellationToken cancellationToken)
        {
            var Result = await tipoVentaRepository.GetByIdAsync(request.Id);
            if (Result == null)
            {
                throw new ApiExteption("No se Encontro El Tipo de Venta", (int)HttpStatusCode.NotFound);
            }

            Result = mapper.Map<TipoVenta>(request);
            await tipoVentaRepository.UpdateAsync(Result, Result.Id);


            TipoVentaResponse response = mapper.Map<TipoVentaResponse>(Result);

            return new Response<TipoVentaResponse>( response);

        }
    }
}
