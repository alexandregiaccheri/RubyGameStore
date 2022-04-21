using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class AtualizarCupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodCupom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TipoDesconto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeUsos = table.Column<int>(type: "int", nullable: false),
                    ValidadeCupom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorDescontoReais = table.Column<double>(type: "float", nullable: false),
                    ValorDescontoPorcento = table.Column<int>(type: "int", nullable: false),
                    ValorMaximoDesconto = table.Column<double>(type: "float", nullable: false),
                    ValorRequerido = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupons");
        }
    }
}
