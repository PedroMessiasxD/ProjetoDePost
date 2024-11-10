using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class AddingPostagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipanteEmpresas_Campanhas_CampanhaId",
                table: "ParticipanteEmpresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Campanhas_ProjetoId",
                table: "Postagens");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "Postagens",
                newName: "CampanhaId");

            migrationBuilder.RenameIndex(
                name: "IX_Postagens_ProjetoId",
                table: "Postagens",
                newName: "IX_Postagens_CampanhaId");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Postagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipanteEmpresas_Campanhas_CampanhaId",
                table: "ParticipanteEmpresas",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Campanhas_CampanhaId",
                table: "Postagens",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipanteEmpresas_Campanhas_CampanhaId",
                table: "ParticipanteEmpresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Campanhas_CampanhaId",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Postagens");

            migrationBuilder.RenameColumn(
                name: "CampanhaId",
                table: "Postagens",
                newName: "ProjetoId");

            migrationBuilder.RenameIndex(
                name: "IX_Postagens_CampanhaId",
                table: "Postagens",
                newName: "IX_Postagens_ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipanteEmpresas_Campanhas_CampanhaId",
                table: "ParticipanteEmpresas",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Campanhas_ProjetoId",
                table: "Postagens",
                column: "ProjetoId",
                principalTable: "Campanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
