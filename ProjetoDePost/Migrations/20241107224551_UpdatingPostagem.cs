using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingPostagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "TomLinguagem",
                table: "Postagens");

            migrationBuilder.RenameColumn(
                name: "PublicoAlvo",
                table: "Campanhas",
                newName: "Descricao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Campanhas",
                newName: "PublicoAlvo");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Postagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TomLinguagem",
                table: "Postagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
