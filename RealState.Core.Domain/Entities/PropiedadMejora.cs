

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Domain.Entities
{
    public class PropiedadMejora
    {
        [Key]

        public int IdPropiedadMejora { get; set; }
        public int IdPropiedad { get; set; }
        public Propiedad Propiedad { get; set; }

        public int IdMejora { get; set; }
        public Mejora Mejora { get; set; }
    }
}
