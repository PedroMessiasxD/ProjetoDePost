using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class AddingCampanha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampanhaId",
                table: "ParticipanteEmpresas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteEmpresas_CampanhaId",
                table: "ParticipanteEmpresas",
                column: "CampanhaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipanteEmpresas_Campanhas_CampanhaId",
                table: "ParticipanteEmpresas",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipanteEmpresas_Campanhas_CampanhaId",
                table: "ParticipanteEmpresas");

            migrationBuilder.DropIndex(
                name: "IX_ParticipanteEmpresas_CampanhaId",
                table: "ParticipanteEmpresas");

            migrationBuilder.DropColumn(
                name: "CampanhaId",
                table: "ParticipanteEmpresas");
        }
    }
}
