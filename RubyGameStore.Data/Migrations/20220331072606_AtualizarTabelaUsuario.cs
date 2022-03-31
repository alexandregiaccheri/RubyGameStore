using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class AtualizarTabelaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuaUsuario",
                table: "AspNetUsers",
                newName: "TelefoneContato");

            migrationBuilder.AddColumn<string>(
                name: "LogradouroUsuario",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogradouroUsuario",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TelefoneContato",
                table: "AspNetUsers",
                newName: "RuaUsuario");
        }
    }
}
