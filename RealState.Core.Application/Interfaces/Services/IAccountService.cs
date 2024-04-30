using RealState.Core.Application.Dtos.Account.Request;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.ViewModel.User;

namespace RealState.Core.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Activar(string UserId, ActivarUser activarUser);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request , bool opc);
        Task<UpdateResponse> EditUserAsync(string UserId, UpdateRequest request);
        Task<IEnumerable<UserViewModel>> GetAllUser();
        Task<UpdateUserViewModel> GetByUserId(string UserId);
        Task LogOut();

        Task<String> ActivarNewAccount(string userId,  ActivarUser activarUser);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterUserAdminAsync(RegisterRequestAdmin request , string UserId, bool opc);
        Task SignOutAsync();
    }
}