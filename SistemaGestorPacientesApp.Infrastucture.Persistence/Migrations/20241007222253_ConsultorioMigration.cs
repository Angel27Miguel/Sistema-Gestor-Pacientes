using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConsultorioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PruebasLaboratorio_Consultorios_ConsultorioId",
                table: "PruebasLaboratorio");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Consultorios_ConsultorioId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "PruebasLaboratorio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "Medicos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "Citas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PruebasLaboratorio_Consultorios_ConsultorioId",
                table: "PruebasLaboratorio",
                column: "ConsultorioId",
                principalTable: "Consultorios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Consultorios_ConsultorioId",
                table: "Usuarios",
                column: "ConsultorioId",
                principalTable: "Consultorios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PruebasLaboratorio_Consultorios_ConsultorioId",
                table: "PruebasLaboratorio");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Consultorios_ConsultorioId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "PruebasLaboratorio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "Medicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsultorioId",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PruebasLaboratorio_Consultorios_ConsultorioId",
                table: "PruebasLaboratorio",
                column: "ConsultorioId",
                principalTable: "Consultorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Consultorios_ConsultorioId",
                table: "Usuarios",
                column: "ConsultorioId",
                principalTable: "Consultorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
