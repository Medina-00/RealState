

using MediatR;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Features.Tipopropiedad.Commands.DeleteTipoPropiedadById;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Tipoventa.Commands.DeleteTipoVentaById
{
    public class DeleteTipoVentaByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTipoVentaByIdCommandHandle : IRequestHandler<DeleteTipoVentaByIdCommand, Response<int>>
    {
        private readonly ITipoVentaRepository tipoVentaRepository;

        public DeleteTipoVentaByIdCommandHandle(ITipoVentaRepository tipoVentaRepository)
        {
            this.tipoVentaRepository = tipoVentaRepository;
        }
        public async Task<Response<int>> Handle(DeleteTipoVentaByIdCommand request, CancellationToken cancellationToken)
        {
            var Result = await tipoVentaRepository.GetByIdAsync(request.Id);
            if (Result == null)
            {
                throw new ApiExteption("No se Encontro El Tipo de Venta", (int)HttpStatusCode.NotFound);
            }

            await tipoVentaRepository.DeleteAsync(Result);

            return new Response<int>( Result.Id);
        }
    }
}
