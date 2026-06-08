using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMaquila.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProgramacionDiariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgracionDiaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineaId = table.Column<int>(type: "int", nullable: false),
                    PrendaId = table.Column<int>(type: "int", nullable: false),
                    SecuenciaDia = table.Column<int>(type: "int", nullable: false),
                    CantidadObjetivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgracionDiaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgracionDiaria_Lineas_LineaId",
                        column: x => x.LineaId,
                        principalTable: "Lineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgracionDiaria_Prendas_PrendaId",
                        column: x => x.PrendaId,
                        principalTable: "Prendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgracionDiaria_LineaId",
                table: "ProgracionDiaria",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgracionDiaria_PrendaId",
                table: "ProgracionDiaria",
                column: "PrendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgracionDiaria");
        }
    }
}
