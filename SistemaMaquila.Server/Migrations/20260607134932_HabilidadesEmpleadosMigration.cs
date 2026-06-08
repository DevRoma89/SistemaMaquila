using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMaquila.Server.Migrations
{
    /// <inheritdoc />
    public partial class HabilidadesEmpleadosMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabilidadesEmpleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    OperacionId = table.Column<int>(type: "int", nullable: false),
                    EficienciaSocio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadesEmpleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilidadesEmpleados_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabilidadesEmpleados_Operaciones_OperacionId",
                        column: x => x.OperacionId,
                        principalTable: "Operaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesEmpleados_EmpleadoId",
                table: "HabilidadesEmpleados",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesEmpleados_OperacionId",
                table: "HabilidadesEmpleados",
                column: "OperacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabilidadesEmpleados");
        }
    }
}
