using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingHistoric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativa",
                table: "Campanhas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativa",
                table: "Campanhas");
        }
    }
}
