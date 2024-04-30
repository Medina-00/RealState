
using RealState.Core.Application.Dtos.Account.Request;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.ViewModel.User;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm , bool opc);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<RegisterResponse> RegisterUserAdminAsync(SaveUserViewModel request, string UserId,bool opc);
        Task SignOutAsync();
        Task<UpdateResponse> UpdateUserAsync(string userId, UpdateUserViewModel request);
        Task<UpdateUserViewModel> GetByUserId(string UserName);

        Task<IEnumerable<UserViewModel>> GetAllUser();

        Task<String> ActivarNewAccount(string userId, ActivarUser activarUser);
        Task<bool> Activar(string UserId, ActivarUser activarUser);

    }
}
