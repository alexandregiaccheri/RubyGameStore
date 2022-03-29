using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class AtualizarUsuarioCNPJ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosCNPJ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuaUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CidadeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEPUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosCNPJ", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosCNPJ");
        }
    }
}
