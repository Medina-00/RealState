

using MediatR;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.mejora.Commands.DeleteMejoraById
{
   
    public class DeleteMejoraByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteMejoraByIdCommandHandle : IRequestHandler<DeleteMejoraByIdCommand, Response<int>>
    {
        private readonly IMejoraRepository mejoraRepository;

        public DeleteMejoraByIdCommandHandle(IMejoraRepository mejoraRepository )
        {
            this.mejoraRepository = mejoraRepository;
        }
        public async Task<Response<int>> Handle(DeleteMejoraByIdCommand request, CancellationToken cancellationToken)
        {
            var Result = await mejoraRepository.GetByIdAsync(request.Id);
            if (Result == null)
            {
                throw new ApiExteption("No se Encontro La mejora",(int)HttpStatusCode.NotFound);
            }

            await mejoraRepository.DeleteAsync(Result);

            return new Response<int>(Result.Id);
        }
    }
}
