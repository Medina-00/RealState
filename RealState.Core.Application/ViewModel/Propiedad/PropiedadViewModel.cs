

using RealState.Core.Application.ViewModel.PropiedadFavorita;
using RealState.Core.Application.ViewModel.PropiedadMejora;
using RealState.Core.Application.ViewModel.TipoPropiedad;
using RealState.Core.Application.ViewModel.TipoVenta;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.ViewModel.Propiedad
{
    public class PropiedadViewModel
    {
        public int IdPropiedad { get; set; }
        public int TipoPropiedadId { get; set; }
        public int TipoVentaId { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public decimal Tamaño { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }

        public DateTime Fecha { get; set; }
        public string Numero6Digitos { get; set; }
        public string UserId { get; set; }

        public string? ImagenPrincipal { get; set; }

        public string? Imagen1 { get; set; }

        public string? Imagen2 { get; set; }

        public string? Imagen3 { get; set; }

        //public ICollection<TipoPropiedadViewModel> TipoPropiedad { get; set; }
        //public ICollection<TipoVentaViewModel> TipoVenta { get; set; }
        //public ICollection<PropiedadMejoraViewModel> propiedadMejoras { get; set; }

        //public ICollection<PropiedadFavoritaViewModel> propiedadesFavoritas { get; set; }
    }
}
