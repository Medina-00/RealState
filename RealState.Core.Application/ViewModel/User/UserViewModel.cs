

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.User
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Rol { get; set; }

        public string ?Cedula { get; set; }

        public string? Foto { get; set; }

        public bool? Activo { get; set; }
        public string? Phone { get; set; }

        public int? CantidadPropiedad { get; set; }


    }
}
