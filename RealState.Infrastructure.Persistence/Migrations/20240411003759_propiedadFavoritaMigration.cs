using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class propiedadFavoritaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavUserId",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "IsFavorita",
                table: "Propiedades");

            migrationBuilder.CreateTable(
                name: "PropiedadFavoritas",
                columns: table => new
                {
                    IdFavorita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadFavoritas", x => x.IdFavorita);
                    table.ForeignKey(
                        name: "FK_PropiedadFavoritas_Propiedades_IdPropiedad",
                        column: x => x.IdPropiedad,
                        principalTable: "Propiedades",
                        principalColumn: "IdPropiedad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadFavoritas_IdPropiedad",
                table: "PropiedadFavoritas",
                column: "IdPropiedad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropiedadFavoritas");

            migrationBuilder.AddColumn<string>(
                name: "FavUserId",
                table: "Propiedades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorita",
                table: "Propiedades",
                type: "bit",
                nullable: true);
        }
    }
}
