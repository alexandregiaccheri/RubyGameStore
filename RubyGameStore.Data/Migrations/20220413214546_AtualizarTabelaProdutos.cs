using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class AtualizarTabelaProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "Preco50",
                table: "Produtos",
                newName: "PrecoPromo");

            migrationBuilder.RenameColumn(
                name: "Preco100",
                table: "Produtos",
                newName: "PrecoNormal");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "PrecoPromo",
                table: "Produtos",
                newName: "Preco50");

            migrationBuilder.RenameColumn(
                name: "PrecoNormal",
                table: "Produtos",
                newName: "Preco100");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "Produtos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
