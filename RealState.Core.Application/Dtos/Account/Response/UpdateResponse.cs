
namespace RealState.Core.Application.Dtos.Account.Response
{
    public class UpdateResponse
    {
        public string UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? Phone { get; set; }

        public string? Foto { get; set; }
        public string? Cedula { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
