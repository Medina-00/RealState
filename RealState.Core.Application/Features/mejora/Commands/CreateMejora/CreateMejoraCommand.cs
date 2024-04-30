

using AutoMapper;
using MediatR;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Features.mejora.Commands.CreateMejora
{
    public class CreateMejoraCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }

    public class CreateMejoraCommandHandle : IRequestHandler<CreateMejoraCommand, Response<int>>
    {
        private readonly IMapper mapper;
        private readonly IMejoraRepository mejoraRepository;

        public CreateMejoraCommandHandle(IMapper mapper , IMejoraRepository mejoraRepository)
        {
            this.mapper = mapper;
            this.mejoraRepository = mejoraRepository;
        }
        public async Task<Response<int>> Handle(CreateMejoraCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<Mejora>(request);
            await mejoraRepository.AddAsync(result);
            return new Response<int> (result.Id);
        }
    }
}
