using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingFrequenceMax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantidadeMaxPostagem",
                table: "Campanhas",
                newName: "FrequenciaMaxima");

            migrationBuilder.RenameColumn(
                name: "FrequenciaPostagem",
                table: "Campanhas",
                newName: "Frequencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FrequenciaMaxima",
                table: "Campanhas",
                newName: "QuantidadeMaxPostagem");

            migrationBuilder.RenameColumn(
                name: "Frequencia",
                table: "Campanhas",
                newName: "FrequenciaPostagem");
        }
    }
}
