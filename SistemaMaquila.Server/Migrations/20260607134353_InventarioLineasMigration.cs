using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMaquila.Server.Migrations
{
    /// <inheritdoc />
    public partial class InventarioLineasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventarioLineas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineaId = table.Column<int>(type: "int", nullable: false),
                    TipoMaquinaId = table.Column<int>(type: "int", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioLineas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioLineas_Lineas_LineaId",
                        column: x => x.LineaId,
                        principalTable: "Lineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventarioLineas_TipoMaquinas_TipoMaquinaId",
                        column: x => x.TipoMaquinaId,
                        principalTable: "TipoMaquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventarioLineas_LineaId",
                table: "InventarioLineas",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioLineas_TipoMaquinaId",
                table: "InventarioLineas",
                column: "TipoMaquinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventarioLineas");
        }
    }
}
