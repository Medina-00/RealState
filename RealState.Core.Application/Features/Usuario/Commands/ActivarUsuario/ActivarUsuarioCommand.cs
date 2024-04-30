

using MediatR;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.User;
using RealState.Core.Application.Wrappers;

namespace RealState.Core.Application.Features.Usuario.Commands.ActivarUsuario
{
    public class ActivarUsuarioCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }

        public string Desicion { get; set; }
    }

    public class ActivarUsuarioCommandHandle : IRequestHandler<ActivarUsuarioCommand, Response<bool>>
    {
        private readonly IUserService userService;

        public ActivarUsuarioCommandHandle(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Response<bool>> Handle(ActivarUsuarioCommand request, CancellationToken cancellationToken)
        {
            ActivarUser activarUser = new();
            activarUser.Activo = request.Desicion;
            var result = await userService.Activar(request.UserId,activarUser);
            return new Response<bool> (result);
        }


        
    }
}
