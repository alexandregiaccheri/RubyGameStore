using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class AtualizarTabelasParaDescontosEFrete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CupomId",
                table: "PedidosCabecalho",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DescontoAplicado",
                table: "PedidosCabecalho",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FreteAplicado",
                table: "PedidosCabecalho",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_PedidosCabecalho_CupomId",
                table: "PedidosCabecalho",
                column: "CupomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosCabecalho_Cupons_CupomId",
                table: "PedidosCabecalho",
                column: "CupomId",
                principalTable: "Cupons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosCabecalho_Cupons_CupomId",
                table: "PedidosCabecalho");

            migrationBuilder.DropIndex(
                name: "IX_PedidosCabecalho_CupomId",
                table: "PedidosCabecalho");

            migrationBuilder.DropColumn(
                name: "CupomId",
                table: "PedidosCabecalho");

            migrationBuilder.DropColumn(
                name: "DescontoAplicado",
                table: "PedidosCabecalho");

            migrationBuilder.DropColumn(
                name: "FreteAplicado",
                table: "PedidosCabecalho");
        }
    }
}
