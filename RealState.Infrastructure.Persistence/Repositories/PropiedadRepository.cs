

using Microsoft.EntityFrameworkCore;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;
using System;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        private readonly ApplicationContext applicationContext;

        public PropiedadRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public override async Task<Propiedad> AddAsync(Propiedad t)
        {
            // Generar el número de 6 dígitos para la propiedad
            t.Numero6Digitos = GenerarNumero();
           

            // Agregar la propiedad al contexto
            applicationContext.Set<Propiedad>().Add(t);

            // Guardar los cambios para obtener el IdPropiedad asignado
            await applicationContext.SaveChangesAsync();

           

            return t;
        }




        public override async Task UpdateAsync(Propiedad t, int id)
        {
            var entity = await applicationContext.Set<Propiedad>().FindAsync(id);
            var mejorasP = await applicationContext.Set<PropiedadMejora>().ToListAsync();
            t.Numero6Digitos = entity!.Numero6Digitos;
            t.Fecha = entity.Fecha;

            if (t.propiedadMejoras != null && t.propiedadMejoras.Count != 0)
            {
                entity!.propiedadMejoras = mejorasP.Where(mp => mp.IdPropiedad == id).ToList();

                if (entity.propiedadMejoras != null && entity.propiedadMejoras.Count != 0)
                {
                    // Eliminar las propiedades mejoradas que ya no están presentes en la lista t.propiedadMejoras
                    foreach (var mejora in entity.propiedadMejoras!.ToList())
                    {
                        if (!t.propiedadMejoras!.Any(pm => pm.IdMejora == mejora.IdMejora))
                        {
                            entity.propiedadMejoras!.Remove(mejora);
                        }
                    }

                    // Agregar nuevas propiedades mejoradas
                    foreach (var mejoraId in t.propiedadMejoras!)
                    {
                        if (!entity.propiedadMejoras!.Any(pm => pm.IdMejora == mejoraId.IdMejora))
                        {
                            var propiedadMejora = new PropiedadMejora
                            {
                                IdPropiedad = entity.IdPropiedad,
                                IdMejora = mejoraId.IdMejora
                            };
                            entity.propiedadMejoras!.Add(propiedadMejora);
                        }
                    }

                    // Guardar los cambios en el contexto
                }
            }
            else
            {
                entity!.propiedadMejoras = mejorasP.Where(mp => mp.IdPropiedad == id).ToList();
            }

            applicationContext.Entry(entity).CurrentValues.SetValues(t!);

            


            await applicationContext.SaveChangesAsync();

        }


        public string GenerarNumero()
        {
            string Numero6Digitos;
            Random random = new Random();
            bool PropiedadUnica = false;

            do
            {
                // Generar un número de cuenta aleatorio
                Numero6Digitos = random.Next(100000, 999999).ToString();

                // Verificar si el número de cuenta ya existe en el sistema
                PropiedadUnica = applicationContext.Propiedades.Any(c => c.Numero6Digitos == Numero6Digitos);
            } while (PropiedadUnica);

            return Numero6Digitos;
        }


    }
}
