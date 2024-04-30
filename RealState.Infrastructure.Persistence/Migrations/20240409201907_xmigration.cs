using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class xmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropiedadesFavoritas");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorita",
                table: "Propiedades",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorita",
                table: "Propiedades");

            migrationBuilder.CreateTable(
                name: "PropiedadesFavoritas",
                columns: table => new
                {
                    IdFavorita = table.Column<int>(type: "int", nullable: false),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadesFavoritas", x => x.IdFavorita);
                    table.ForeignKey(
                        name: "FK_PropiedadesFavoritas_Propiedades_IdFavorita",
                        column: x => x.IdFavorita,
                        principalTable: "Propiedades",
                        principalColumn: "IdPropiedad",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
