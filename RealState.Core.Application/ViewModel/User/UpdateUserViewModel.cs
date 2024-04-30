

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.User
{
    public class UpdateUserViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string? Apellido { get; set; }



        public string ?Cedula { get; set; }

        [DataType(DataType.Text)]
        public string? Phone { get; set; }
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ?Email { get; set; }


        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [DataType(DataType.Password)]

        public string? ConfirmPassword { get; set; }
        public IFormFile? File { get; set; }

        public string? Foto { get; set; }

        public string? Rol { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
