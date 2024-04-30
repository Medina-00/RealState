﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealState.Infrastructure.Persistence.Context;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240405203656_primeraMigration")]
    partial class primeraMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("Favorita")
                        .HasColumnType("bit");

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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdPropiedad");

                    b.HasIndex("TipoPropiedadId");

                    b.HasIndex("TipoVentaId");

                    b.HasIndex("UserId");

                    b.ToTable("Properties", "RealState");
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

            modelBuilder.Entity("RealState.Infrastructure.Identity.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cedula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
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

                    b.HasOne("RealState.Infrastructure.Identity.Entities.ApplicationUser", null)
                        .WithMany("Propiedades")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoPropiedad");

                    b.Navigation("TipoVenta");
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

            modelBuilder.Entity("RealState.Infrastructure.Identity.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Propiedades");
                });
#pragma warning restore 612, 618
        }
    }
}
