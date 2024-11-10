﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDePost.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingHistoric40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "HistoricoCampanhas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "HistoricoCampanhas");
        }
    }
}
