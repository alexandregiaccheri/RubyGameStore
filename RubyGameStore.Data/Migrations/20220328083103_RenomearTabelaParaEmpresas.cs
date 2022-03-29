using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class RenomearTabelaParaEmpresas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsuariosCNPJ_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosCNPJ",
                table: "UsuariosCNPJ");

            migrationBuilder.RenameTable(
                name: "UsuariosCNPJ",
                newName: "Empresas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas");

            migrationBuilder.RenameTable(
                name: "Empresas",
                newName: "UsuariosCNPJ");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosCNPJ",
                table: "UsuariosCNPJ",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsuariosCNPJ_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "UsuariosCNPJ",
                principalColumn: "Id");
        }
    }
}
