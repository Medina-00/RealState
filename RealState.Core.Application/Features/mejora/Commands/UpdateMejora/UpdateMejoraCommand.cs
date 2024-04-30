

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;
using System.Net;

namespace RealState.Core.Application.Features.mejora.Commands.UpdateMejora
{
    public class UpdateMejoraCommand : IRequest<Response<UpdateMejora>>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateMejoraCommandHandle : IRequestHandler<UpdateMejoraCommand, Response<UpdateMejora>>
    {
        private readonly IMejoraRepository mejoraRepository;
        private readonly IMapper mapper;

        public UpdateMejoraCommandHandle(IMejoraRepository mejoraRepository, IMapper mapper)
        {
            this.mejoraRepository = mejoraRepository;
            this.mapper = mapper;
        }
        
        public async Task<Response<UpdateMejora>> Handle(UpdateMejoraCommand request, CancellationToken cancellationToken)
        {
            var Result = await mejoraRepository.GetByIdAsync(request.Id);
            if (Result == null)
            {
                throw new ApiExteption("No se Encontro La Mejora", (int)HttpStatusCode.NotFound);
            }

            Result = mapper.Map<Mejora>(request);
            await mejoraRepository.UpdateAsync(Result, Result.Id);


            UpdateMejora response = mapper.Map<UpdateMejora>(Result);

            return new Response<UpdateMejora>(response);
        }
    }
}
