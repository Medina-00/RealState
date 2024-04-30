

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.User
{
    public class SaveUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar un Rol")]
        [DataType(DataType.Password)]
        public string Rol { get; set; }

        
        public string? Phone { get; set; }


        //[Required(ErrorMessage = "Debe colocar su Foto")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? Foto { get; set; }
        public string ?Cedula { get; set; }

        public bool? Activo { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }


    }
}
