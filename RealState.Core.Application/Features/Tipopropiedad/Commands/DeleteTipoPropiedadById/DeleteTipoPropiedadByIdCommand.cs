

using MediatR;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Tipopropiedad.Commands.DeleteTipoPropiedadById
{
    public class DeleteTipoPropiedadByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTipoPropiedadByIdCommandHandle : IRequestHandler<DeleteTipoPropiedadByIdCommand, Response<int>>
    {
        private readonly ITipoPropiedadRepository tipoPropiedadRepository;

        public DeleteTipoPropiedadByIdCommandHandle(ITipoPropiedadRepository tipoPropiedadRepository)
        {
            this.tipoPropiedadRepository = tipoPropiedadRepository;
        }
        public async Task<Response<int>> Handle(DeleteTipoPropiedadByIdCommand request, CancellationToken cancellationToken)
        {
            var Result = await tipoPropiedadRepository.GetByIdAsync(request.Id);
            if (Result == null)
            {
                throw new ApiExteption("No se Encontro El Tipo de Propiedad", (int)HttpStatusCode.NotFound);
            }

            await tipoPropiedadRepository.DeleteAsync(Result);

            return new Response<int>(Result.Id);
        }
    }
}
