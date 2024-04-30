

using AutoMapper;
using MediatR;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Exceptions;
using RealState.Core.Application.Features.Usuario.Queries.GetAllUsuario;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.User;
using RealState.Core.Application.Wrappers;
using System.Net;

namespace RealState.Core.Application.Features.Usuario.Queries.GetUsuarioById
{
    public class GetUsuarioByIdQuery : IRequest<Response<UpdateUserViewModel>>
    {
        public string UserId { get; set; }
    }

    public class GetUsuarioByIdQueryHandle : IRequestHandler<GetUsuarioByIdQuery, Response<UpdateUserViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public GetUsuarioByIdQueryHandle(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        public async Task<Response<UpdateUserViewModel>> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            var Users = await GetViewModelById(request);

            if (Users == null) throw new ApiExteption("No encontro Usuarios", (int)HttpStatusCode.NotFound);
            return new Response<UpdateUserViewModel>(Users);
        }


        private async Task<UpdateUserViewModel> GetViewModelById(GetUsuarioByIdQuery query)
        {
            var entityList = await userService.GetByUserId(query.UserId);

            return entityList;
            //return mapper.Map<UpdateUserViewModel>(entityList);
        }
    }
}
