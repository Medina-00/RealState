
using Microsoft.AspNetCore.Identity;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        
            public string? Nombre { get; set; }

            public string? Apellido { get; set; }

            public string? Cedula { get; set; }

             public string? Foto { get; set; }


             public bool? Activo { get; set; }


            public ICollection<Propiedad> ?Propiedades { get; set; }

           




        

    }
}
