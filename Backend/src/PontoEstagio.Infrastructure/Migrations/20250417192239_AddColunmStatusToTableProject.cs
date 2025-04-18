using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoEstagio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColunmStatusToTableProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Projetos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projetos");
        }
    }
}
