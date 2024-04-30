

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Domain.Entities
{
    public class PropiedadFavorita
    {
        [Key]
        public int IdFavorita { get; set; }
        public int IdPropiedad { get; set; }
        public string UserId { get; set; }

        public Propiedad ?Propiedad { get; set; }    


    }
}
