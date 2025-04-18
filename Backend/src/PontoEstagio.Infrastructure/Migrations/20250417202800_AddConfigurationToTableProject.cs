using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoEstagio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationToTableProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Usuarios_CreatorId",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_CreatorId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Projetos");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_CreatedBy",
                table: "Projetos",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Usuarios_CreatedBy",
                table: "Projetos",
                column: "CreatedBy",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Usuarios_CreatedBy",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_CreatedBy",
                table: "Projetos");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Projetos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
