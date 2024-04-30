
using Microsoft.AspNetCore.Http;
using RealState.Core.Application.ViewModel.Mejora;
using RealState.Core.Application.ViewModel.TipoPropiedad;
using RealState.Core.Application.ViewModel.TipoVenta;
using RealState.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.Propiedad
{
    public class SavePropiedadViewModel
    {
        public int IdPropiedad { get; set; }
        [Required(ErrorMessage = "Debe Seleccionar Un Tipo de Propiedad")]
        public int TipoPropiedadId { get; set; }
        [Required(ErrorMessage = "Debe Seleccionar Un Tipo de Propiedad")]
        public int TipoVentaId { get; set; }
        [Required(ErrorMessage = "Debe Ingresar El Precio")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Debe Ingresar la Descripcion")]

        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Debe Ingresar el Tamaño")]

        public decimal Tamaño { get; set; }
        [Required(ErrorMessage = "Debe Ingresar la Cantidad de Habitacion")]

        public int CantidadHabitaciones { get; set; }
        [Required(ErrorMessage = "Debe Ingresar la Cantidad de Baños")]

        public int CantidadBaños { get; set; }

        public string UserId { get; set; }
        

        public string? ImagenPrincipal { get; set; }

        public string? Imagen1 { get; set; }

        public string? Imagen2 { get; set; }

        public string? Imagen3 { get; set; }

        public string Numero6Digitos { get; set; }


        [DataType(DataType.Upload)]
        public IFormFile? FilePrincipal { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File1 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File2 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File3 { get; set; }
        [Required(ErrorMessage = "Debe Ingresar ni Aunque sea una Mejora")]
        public ICollection<int> propiedadMejoras { get; set; }
        public ICollection<MejoraViewModel> Mejoras { get; set; }
        public ICollection<TipoPropiedadViewModel> TipoPropiedad { get; set; }
        public ICollection<TipoVentaViewModel> TipoVenta { get; set; }

    }
}
