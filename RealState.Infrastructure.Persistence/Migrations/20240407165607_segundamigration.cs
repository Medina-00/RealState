using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class segundamigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorita",
                schema: "RealState",
                table: "Properties");

            migrationBuilder.CreateTable(
                name: "PropiedadesFavoritas",
                columns: table => new
                {
                    IdFavorita = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadesFavoritas", x => x.IdFavorita);
                    table.ForeignKey(
                        name: "FK_PropiedadesFavoritas_Properties_IdFavorita",
                        column: x => x.IdFavorita,
                        principalSchema: "RealState",
                        principalTable: "Properties",
                        principalColumn: "IdPropiedad",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropiedadesFavoritas");

            migrationBuilder.AddColumn<bool>(
                name: "Favorita",
                schema: "RealState",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
