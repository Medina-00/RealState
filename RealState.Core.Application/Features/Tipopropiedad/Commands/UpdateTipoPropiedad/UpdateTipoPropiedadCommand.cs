

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Wrappers;
using RealState.Core.Domain.Entities;
using System.Net;

namespace RealState.Core.Application.Features.Tipopropiedad.Commands.UpdateTipoPropiedad
{
    public class UpdateTipoPropiedadCommand : IRequest<Response<TipoPropiedadResponse>>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateTipoPropiedadCommandHandle : IRequestHandler<UpdateTipoPropiedadCommand, Response<TipoPropiedadResponse>>
    {
        private readonly ITipoPropiedadRepository tipoPropiedadRepository;
        private readonly IMapper mapper;

        public UpdateTipoPropiedadCommandHandle(ITipoPropiedadRepository tipoPropiedadRepository,IMapper mapper)
        {
            this.tipoPropiedadRepository = tipoPropiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<TipoPropiedadResponse>> Handle(UpdateTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            var Result = await tipoPropiedadRepository.GetByIdAsync(request.Id);
            if(Result == null)
            {
                throw new ApiExteption("No se Encontro El Tipo de Propiedad", (int)HttpStatusCode.NotFound);
            }

            Result = mapper.Map<TipoPropiedad>(request);
            await tipoPropiedadRepository.UpdateAsync(Result, Result.Id);


            TipoPropiedadResponse response = mapper.Map<TipoPropiedadResponse>(Result);

            return new Response<TipoPropiedadResponse>(response);
            
        }
    }
}
