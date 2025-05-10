using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoEstagio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateActivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Frequencias_AttendanceId",
                table: "Atividades");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Atividades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Frequencias_AttendanceId",
                table: "Atividades",
                column: "AttendanceId",
                principalTable: "Frequencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Frequencias_AttendanceId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Atividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Frequencias_AttendanceId",
                table: "Atividades",
                column: "AttendanceId",
                principalTable: "Frequencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
