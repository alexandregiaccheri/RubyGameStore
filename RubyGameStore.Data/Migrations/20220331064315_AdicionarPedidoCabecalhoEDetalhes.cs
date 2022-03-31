using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class AdicionarPedidoCabecalhoEDetalhes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidosCabecalho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataHoraPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPedido = table.Column<double>(type: "float", nullable: false),
                    StatusPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodRastreio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transportadora = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneContato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeDestinatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogradouroEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CidadeEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEPEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosCabecalho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosCabecalho_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDetalhes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDetalhes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhes_PedidosCabecalho_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PedidosCabecalho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosCabecalho_UsuarioId",
                table: "PedidosCabecalho",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhes_PedidoId",
                table: "PedidosDetalhes",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhes_ProdutoId",
                table: "PedidosDetalhes",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosDetalhes");

            migrationBuilder.DropTable(
                name: "PedidosCabecalho");
        }
    }
}
