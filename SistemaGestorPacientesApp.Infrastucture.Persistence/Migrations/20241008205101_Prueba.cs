using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PruebaLaboratorioId",
                table: "ResultadosLaboratorio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio",
                column: "PruebaLaboratorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio",
                column: "PruebaLaboratorioId",
                principalTable: "PruebasLaboratorio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio");

            migrationBuilder.DropIndex(
                name: "IX_ResultadosLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio");

            migrationBuilder.DropColumn(
                name: "PruebaLaboratorioId",
                table: "ResultadosLaboratorio");
        }
    }
}
