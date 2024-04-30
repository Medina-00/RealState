

using RealState.Core.Domain.Commun;

namespace RealState.Core.Domain.Entities
{
    public class TipoPropiedad : Base
    {
        public int ?Cantidad { get; set; }

        public ICollection<Propiedad> Propiedades { get; set; }
    }
}
