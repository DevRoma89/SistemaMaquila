using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMaquila.Server.Migrations
{
    /// <inheritdoc />
    public partial class OPMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAMEstimado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoMaquinaId = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operaciones_TipoMaquinas_TipoMaquinaId",
                        column: x => x.TipoMaquinaId,
                        principalTable: "TipoMaquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperacionPrendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrendaId = table.Column<int>(type: "int", nullable: false),
                    OrdenSecuencia = table.Column<int>(type: "int", nullable: false),
                    OperacionId = table.Column<int>(type: "int", nullable: false),
                    SAMReal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperacionPrendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperacionPrendas_Operaciones_OperacionId",
                        column: x => x.OperacionId,
                        principalTable: "Operaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperacionPrendas_Prendas_PrendaId",
                        column: x => x.PrendaId,
                        principalTable: "Prendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_TipoMaquinaId",
                table: "Operaciones",
                column: "TipoMaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_OperacionPrendas_OperacionId",
                table: "OperacionPrendas",
                column: "OperacionId");

            migrationBuilder.CreateIndex(
                name: "IX_OperacionPrendas_PrendaId",
                table: "OperacionPrendas",
                column: "PrendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperacionPrendas");

            migrationBuilder.DropTable(
                name: "Operaciones");
        }
    }
}
