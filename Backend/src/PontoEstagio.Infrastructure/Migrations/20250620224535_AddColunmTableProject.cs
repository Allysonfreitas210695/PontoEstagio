using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoEstagio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColunmTableProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Projects",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Projects");
        }
    }
}
