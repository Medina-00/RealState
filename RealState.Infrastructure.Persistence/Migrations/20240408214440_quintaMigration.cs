using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class quintaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_TipoPropiedades_TipoPropiedadId",
                schema: "RealState",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_TipoVentas_TipoVentaId",
                schema: "RealState",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserId",
                schema: "RealState",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadesFavoritas_Properties_IdFavorita",
                table: "PropiedadesFavoritas");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Properties_IdPropiedad",
                table: "PropiedadMejoras");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                schema: "RealState",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UserId",
                schema: "RealState",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "Properties",
                schema: "RealState",
                newName: "Propiedades");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_TipoVentaId",
                table: "Propiedades",
                newName: "IX_Propiedades_TipoVentaId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_TipoPropiedadId",
                table: "Propiedades",
                newName: "IX_Propiedades_TipoPropiedadId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Propiedades",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propiedades",
                table: "Propiedades",
                column: "IdPropiedad");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_TipoPropiedades_TipoPropiedadId",
                table: "Propiedades",
                column: "TipoPropiedadId",
                principalTable: "TipoPropiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_TipoVentas_TipoVentaId",
                table: "Propiedades",
                column: "TipoVentaId",
                principalTable: "TipoVentas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadesFavoritas_Propiedades_IdFavorita",
                table: "PropiedadesFavoritas",
                column: "IdFavorita",
                principalTable: "Propiedades",
                principalColumn: "IdPropiedad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_IdPropiedad",
                table: "PropiedadMejoras",
                column: "IdPropiedad",
                principalTable: "Propiedades",
                principalColumn: "IdPropiedad",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_TipoPropiedades_TipoPropiedadId",
                table: "Propiedades");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_TipoVentas_TipoVentaId",
                table: "Propiedades");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadesFavoritas_Propiedades_IdFavorita",
                table: "PropiedadesFavoritas");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_IdPropiedad",
                table: "PropiedadMejoras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propiedades",
                table: "Propiedades");

            migrationBuilder.EnsureSchema(
                name: "RealState");

            migrationBuilder.RenameTable(
                name: "Propiedades",
                newName: "Properties",
                newSchema: "RealState");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedades_TipoVentaId",
                schema: "RealState",
                table: "Properties",
                newName: "IX_Properties_TipoVentaId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedades_TipoPropiedadId",
                schema: "RealState",
                table: "Properties",
                newName: "IX_Properties_TipoPropiedadId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "RealState",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                schema: "RealState",
                table: "Properties",
                column: "IdPropiedad");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                schema: "RealState",
                table: "Properties",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_TipoPropiedades_TipoPropiedadId",
                schema: "RealState",
                table: "Properties",
                column: "TipoPropiedadId",
                principalTable: "TipoPropiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_TipoVentas_TipoVentaId",
                schema: "RealState",
                table: "Properties",
                column: "TipoVentaId",
                principalTable: "TipoVentas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserId",
                schema: "RealState",
                table: "Properties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadesFavoritas_Properties_IdFavorita",
                table: "PropiedadesFavoritas",
                column: "IdFavorita",
                principalSchema: "RealState",
                principalTable: "Properties",
                principalColumn: "IdPropiedad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Properties_IdPropiedad",
                table: "PropiedadMejoras",
                column: "IdPropiedad",
                principalSchema: "RealState",
                principalTable: "Properties",
                principalColumn: "IdPropiedad",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
