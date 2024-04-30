

using AutoMapper;
using MediatR;

using RealState.Core.Application.Exceptions;

using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.User;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Usuario.Queries.GetAllUsuario
{
    public class GetAllUsuarioQuery : IRequest<Response<ICollection<UserViewModel>>>
    {
    }

    public class GetAllUsuarioQueryHandle : IRequestHandler<GetAllUsuarioQuery, Response<ICollection<UserViewModel>>>
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public GetAllUsuarioQueryHandle(IMapper mapper , IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        public async Task<Response<ICollection<UserViewModel>>> Handle(GetAllUsuarioQuery request, CancellationToken cancellationToken)
        {
            var Users = await GetAllViewModel(request);

            if (Users == null ) throw new ApiExteption("No encontro Usuarios", (int)HttpStatusCode.NotFound);
            return new Response<ICollection<UserViewModel>>(Users);
        }


        private async Task<List<UserViewModel>> GetAllViewModel(GetAllUsuarioQuery query)
        {
            var entityList = await userService.GetAllUser();
           

            return mapper.Map<List<UserViewModel>>(entityList);
        }
    }
}
