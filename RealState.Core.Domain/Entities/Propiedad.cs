

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Domain.Entities
{
    public class Propiedad
    {
        [Key]
        public int IdPropiedad { get; set; }
        public int TipoPropiedadId { get; set; }
        public int TipoVentaId { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public decimal Tamaño { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Numero6Digitos { get; set; }
        public string UserId { get; set; }

        public string ?ImagenPrincipal { get; set; }

        public string ?Imagen1 { get; set; }

        public string ?Imagen2 { get; set; }

        public string ?Imagen3 { get; set; }

        public TipoPropiedad TipoPropiedad { get; set; }
        public TipoVenta TipoVenta { get; set; }
        public ICollection<PropiedadMejora> ?propiedadMejoras { get; set; }
        public ICollection<PropiedadFavorita> ?PropiedadFavoritas { get; set; }

       
    }
}
