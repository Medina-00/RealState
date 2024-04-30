

using RealState.Core.Application.Dtos.Email;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }

}
