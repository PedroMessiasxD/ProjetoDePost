using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class Milagre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "Frequencia",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "Tema",
                table: "Postagens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Postagens",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Frequencia",
                table: "Postagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tema",
                table: "Postagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
