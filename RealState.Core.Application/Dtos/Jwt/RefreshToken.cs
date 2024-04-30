
namespace RealState.Core.Application.Dtos.Jwt
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? Expires { get; set; }
        //Esto es para verificar ya expiro el token.
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string? ReplacedByToken { get; set; }
        //Aqui verifo si el token esta activo a la hora de pasar por aqui.
        public bool IsActive => Revoked == null && !IsExpired;

    }
}
