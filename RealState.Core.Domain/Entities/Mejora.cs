
using RealState.Core.Domain.Commun;

namespace RealState.Core.Domain.Entities
{
    public class Mejora : Base
    {
        public ICollection<PropiedadMejora> propiedadMejoras { get; set; }
    }
}
