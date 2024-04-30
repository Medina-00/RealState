

using AutoMapper;
using RealState.Core.Application.Dtos.Account.Request;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Interfaces;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.User;
using System.Runtime.CompilerServices;

namespace RealState.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public UserService(IMapper mapper, IAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<bool> Activar(string UserId, ActivarUser activarUser)
        {
            return await accountService.Activar(UserId, activarUser);
        }

        public async Task<string> ActivarNewAccount(string userId,  ActivarUser activarUser)
        {
            return await accountService.ActivarNewAccount(userId, activarUser);

        }

        public async Task<IEnumerable<UserViewModel>> GetAllUser()
        {
            return await accountService.GetAllUser();
        }

        public async Task<UpdateUserViewModel> GetByUserId(string UserName)
        {
            return await accountService.GetByUserId(UserName);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm , bool opc)
        {
            AuthenticationRequest request = mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse response = await accountService.AuthenticateAsync(request, opc);
            return response;
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
           
                RegisterRequest request = mapper.Map<RegisterRequest>(vm);
                return await accountService.RegisterUserAsync(request, origin);
           
            
        }

        public async Task<RegisterResponse> RegisterUserAdminAsync(SaveUserViewModel vm, string UserId, bool opc)
        {
            if(opc)
            {
                RegisterRequestAdmin request = mapper.Map<RegisterRequestAdmin>(vm);
                return await accountService.RegisterUserAdminAsync(request, UserId,true);
            }
            else
            {
                RegisterRequestAdmin request = mapper.Map<RegisterRequestAdmin>(vm);
                return await accountService.RegisterUserAdminAsync(request, UserId,false);
            }
           
        }

        public async Task SignOutAsync()
        {
            await accountService.LogOut();
        }

        public async Task<UpdateResponse> UpdateUserAsync(string userId, UpdateUserViewModel updateUser)
        {
            UpdateRequest request = mapper.Map<UpdateRequest>(updateUser);

            return await accountService.EditUserAsync(userId, request);
        }


    }
}
