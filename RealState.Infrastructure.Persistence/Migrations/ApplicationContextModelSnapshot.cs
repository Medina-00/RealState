﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealState.Infrastructure.Persistence.Context;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealState.Core.Domain.Entities.Mejora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mejoras", (string)null);
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.Propiedad", b =>
                {
                    b.Property<int>("IdPropiedad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPropiedad"));

                    b.Property<int>("CantidadBaños")
                        .HasColumnType("int");

                    b.Property<int>("CantidadHabitaciones")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenPrincipal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero6Digitos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tamaño")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoPropiedadId")
                        .HasColumnType("int");

                    b.Property<int>("TipoVentaId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPropiedad");

                    b.HasIndex("TipoPropiedadId");

                    b.HasIndex("TipoVentaId");

                    b.ToTable("Propiedades", (string)null);
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.PropiedadFavorita", b =>
                {
                    b.Property<int>("IdFavorita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFavorita"));

                    b.Property<int>("IdPropiedad")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFavorita");

                    b.HasIndex("IdPropiedad");

                    b.ToTable("PropiedadFavoritas", (string)null);
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.PropiedadMejora", b =>
                {
                    b.Property<int>("IdPropiedad")
                        .HasColumnType("int");

                    b.Property<int>("IdMejora")
                        .HasColumnType("int");

                    b.Property<int>("IdPropiedadMejora")
                        .HasColumnType("int");

                    b.HasKey("IdPropiedad", "IdMejora");

                    b.HasIndex("IdMejora");

                    b.ToTable("PropiedadMejoras", (string)null);
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.TipoPropiedad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoPropiedades", (string)null);
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.TipoVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoVentas", (string)null);
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.Propiedad", b =>
                {
                    b.HasOne("RealState.Core.Domain.Entities.TipoPropiedad", "TipoPropiedad")
                        .WithMany("Propiedades")
                        .HasForeignKey("TipoPropiedadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealState.Core.Domain.Entities.TipoVenta", "TipoVenta")
                        .WithMany("Propiedades")
                        .HasForeignKey("TipoVentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoPropiedad");

                    b.Navigation("TipoVenta");
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.PropiedadFavorita", b =>
                {
                    b.HasOne("RealState.Core.Domain.Entities.Propiedad", "Propiedad")
                        .WithMany("PropiedadFavoritas")
                        .HasForeignKey("IdPropiedad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Propiedad");
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.PropiedadMejora", b =>
                {
                    b.HasOne("RealState.Core.Domain.Entities.Mejora", "Mejora")
                        .WithMany("propiedadMejoras")
                        .HasForeignKey("IdMejora")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealState.Core.Domain.Entities.Propiedad", "Propiedad")
                        .WithMany("propiedadMejoras")
                        .HasForeignKey("IdPropiedad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mejora");

                    b.Navigation("Propiedad");
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.Mejora", b =>
                {
                    b.Navigation("propiedadMejoras");
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.Propiedad", b =>
                {
                    b.Navigation("PropiedadFavoritas");

                    b.Navigation("propiedadMejoras");
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.TipoPropiedad", b =>
                {
                    b.Navigation("Propiedades");
                });

            modelBuilder.Entity("RealState.Core.Domain.Entities.TipoVenta", b =>
                {
                    b.Navigation("Propiedades");
                });
#pragma warning restore 612, 618
        }
    }
}
