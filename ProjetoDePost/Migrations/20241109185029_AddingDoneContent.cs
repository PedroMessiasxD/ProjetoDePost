using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class AddingDoneContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConteudoGerado",
                table: "Postagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConteudoGerado",
                table: "Postagens");
        }
    }
}
