using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class DatabaseOverhaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Plataformas_PlataformaId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Plataformas");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_PlataformaId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PrecoListado",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "PlataformaId",
                table: "Produtos",
                newName: "Metascore");

            migrationBuilder.RenameColumn(
                name: "ImgCapaUrl",
                table: "Produtos",
                newName: "Screenshot6");

            migrationBuilder.AddColumn<string>(
                name: "BoxArt",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Classificacao",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Jogadores",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkTrailer",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Plataforma",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Screenshot1",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Screenshot2",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Screenshot3",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Screenshot4",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Screenshot5",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxArt",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Classificacao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Jogadores",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "LinkTrailer",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Plataforma",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Screenshot1",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Screenshot2",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Screenshot3",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Screenshot4",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Screenshot5",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "Screenshot6",
                table: "Produtos",
                newName: "ImgCapaUrl");

            migrationBuilder.RenameColumn(
                name: "Metascore",
                table: "Produtos",
                newName: "PlataformaId");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PrecoListado",
                table: "Produtos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plataformas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePlataforma = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataformas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_PlataformaId",
                table: "Produtos",
                column: "PlataformaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Plataformas_PlataformaId",
                table: "Produtos",
                column: "PlataformaId",
                principalTable: "Plataformas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
