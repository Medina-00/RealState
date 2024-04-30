

using System.Text.Json.Serialization;

namespace RealState.Core.Application.Dtos.Account.Response
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? Cedula { get; set; }

        public string ?Foto { get; set; }   
        public bool? Activo { get; set; }

        public string? Phone { get; set; }

        public string? JWToken { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }




    }
}
