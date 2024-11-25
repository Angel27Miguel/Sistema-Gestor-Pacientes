using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResultadoLaboratorioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio");

            migrationBuilder.AddColumn<int>(
                name: "PruebaLaboratorioId1",
                table: "ResultadosLaboratorio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosLaboratorio_PruebaLaboratorioId1",
                table: "ResultadosLaboratorio",
                column: "PruebaLaboratorioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio",
                column: "PruebaLaboratorioId",
                principalTable: "PruebasLaboratorio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId1",
                table: "ResultadosLaboratorio",
                column: "PruebaLaboratorioId1",
                principalTable: "PruebasLaboratorio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId1",
                table: "ResultadosLaboratorio");

            migrationBuilder.DropIndex(
                name: "IX_ResultadosLaboratorio_PruebaLaboratorioId1",
                table: "ResultadosLaboratorio");

            migrationBuilder.DropColumn(
                name: "PruebaLaboratorioId1",
                table: "ResultadosLaboratorio");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadosLaboratorio_PruebasLaboratorio_PruebaLaboratorioId",
                table: "ResultadosLaboratorio",
                column: "PruebaLaboratorioId",
                principalTable: "PruebasLaboratorio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
