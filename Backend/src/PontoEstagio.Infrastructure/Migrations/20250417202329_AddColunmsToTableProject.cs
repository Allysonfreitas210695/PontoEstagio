using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoEstagio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColunmsToTableProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Projetos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Projetos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Projetos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Projetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_CreatorId",
                table: "Projetos",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Usuarios_CreatorId",
                table: "Projetos",
                column: "CreatorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Usuarios_CreatorId",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_CreatorId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Projetos");
        }
    }
}
