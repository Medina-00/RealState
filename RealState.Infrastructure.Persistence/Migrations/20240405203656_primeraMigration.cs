using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class primeraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RealState");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mejoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mejoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPropiedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPropiedades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "RealState",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPropiedadId = table.Column<int>(type: "int", nullable: false),
                    TipoVentaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamaño = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadHabitaciones = table.Column<int>(type: "int", nullable: false),
                    CantidadBaños = table.Column<int>(type: "int", nullable: false),
                    Numero6Digitos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Favorita = table.Column<bool>(type: "bit", nullable: false),
                    ImagenPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.IdPropiedad);
                    table.ForeignKey(
                        name: "FK_Properties_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_TipoPropiedades_TipoPropiedadId",
                        column: x => x.TipoPropiedadId,
                        principalTable: "TipoPropiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_TipoVentas_TipoVentaId",
                        column: x => x.TipoVentaId,
                        principalTable: "TipoVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadMejoras",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    IdMejora = table.Column<int>(type: "int", nullable: false),
                    IdPropiedadMejora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadMejoras", x => new { x.IdPropiedad, x.IdMejora });
                    table.ForeignKey(
                        name: "FK_PropiedadMejoras_Mejoras_IdMejora",
                        column: x => x.IdMejora,
                        principalTable: "Mejoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropiedadMejoras_Properties_IdPropiedad",
                        column: x => x.IdPropiedad,
                        principalSchema: "RealState",
                        principalTable: "Properties",
                        principalColumn: "IdPropiedad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TipoPropiedadId",
                schema: "RealState",
                table: "Properties",
                column: "TipoPropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TipoVentaId",
                schema: "RealState",
                table: "Properties",
                column: "TipoVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                schema: "RealState",
                table: "Properties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadMejoras_IdMejora",
                table: "PropiedadMejoras",
                column: "IdMejora");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropiedadMejoras");

            migrationBuilder.DropTable(
                name: "Mejoras");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "RealState");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "TipoPropiedades");

            migrationBuilder.DropTable(
                name: "TipoVentas");
        }
    }
}
