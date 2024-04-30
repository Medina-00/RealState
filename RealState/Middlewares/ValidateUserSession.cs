using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Helpers;

namespace RealState.Middlewares
{
    public  class ValidateUserSession
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse authentication = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (authentication == null)
            {
                return false;
            }
            return true;
        }


    }
}
