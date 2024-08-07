﻿

namespace RealState.Core.Application.Dtos.Account.Request
{
    public class UpdateRequest
    {
        public string UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? Phone { get; set; }

        public string? Cedula { get; set; }

        public string? Foto { get; set; }


    }
}
