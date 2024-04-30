using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Mejora> Mejoras { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<TipoPropiedad> TipoPropiedades { get; set; }
        public DbSet<TipoVenta> TipoVentas { get; set; }
        public DbSet<PropiedadMejora> PropiedadMejoras { get; set; }

        public DbSet<PropiedadFavorita> PropiedadFavoritas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TABLES
            modelBuilder.Entity<Mejora>().ToTable("Mejoras"); 
            modelBuilder.Entity<Propiedad>().ToTable("Propiedades");
            modelBuilder.Entity<TipoPropiedad>().ToTable("TipoPropiedades");
            modelBuilder.Entity<TipoVenta>().ToTable("TipoVentas");
            modelBuilder.Entity<PropiedadMejora>().ToTable("PropiedadMejoras");
            modelBuilder.Entity<PropiedadFavorita>().ToTable("PropiedadFavoritas");



            //KEYS
            modelBuilder.Entity<Mejora>().HasKey(m => m.Id);
            modelBuilder.Entity<TipoPropiedad>().HasKey(m => m.Id);
            modelBuilder.Entity<TipoVenta>().HasKey(m => m.Id);
            modelBuilder.Entity<Propiedad>().HasKey(m => m.IdPropiedad);
            modelBuilder.Entity<PropiedadMejora>().HasKey(m => m.IdPropiedadMejora);
            modelBuilder.Entity<PropiedadFavorita>().HasKey(m => m.IdFavorita);

            // RELACIONES
            modelBuilder.Entity<PropiedadMejora>()
                .HasKey(pc => new { pc.IdPropiedad, pc.IdMejora }); 

            modelBuilder.Entity<PropiedadMejora>()
                .HasOne(pc => pc.Mejora)
                .WithMany(m => m.propiedadMejoras)
                .HasForeignKey(pc => pc.IdMejora);

            modelBuilder.Entity<PropiedadMejora>()
                .HasOne(pc => pc.Propiedad)
                .WithMany(p => p.propiedadMejoras)
                .HasForeignKey(pc => pc.IdPropiedad);

            modelBuilder.Entity<TipoVenta>()
                .HasMany(tv => tv.Propiedades)
                .WithOne(p => p.TipoVenta)
                .HasForeignKey(p => p.TipoVentaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoPropiedad>()
                .HasMany(tp => tp.Propiedades)
                .WithOne(p => p.TipoPropiedad)
                .HasForeignKey(p => p.TipoPropiedadId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Propiedad>()
                .HasMany(p => p.PropiedadFavoritas)
                .WithOne(p => p.Propiedad)
                .HasForeignKey(p => p.IdPropiedad)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
