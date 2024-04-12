using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessoManymindsBack.Migrations
{
    /// <inheritdoc />
    public partial class PedidosCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidosCompras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoPedido",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProdutoCodigo = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidosComprasId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_PedidosCompras_PedidosComprasId",
                        column: x => x.PedidosComprasId,
                        principalTable: "PedidosCompras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Produtos_ProdutoCodigo",
                        column: x => x.ProdutoCodigo,
                        principalTable: "Produtos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_PedidosComprasId",
                table: "ProdutoPedido",
                column: "PedidosComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_ProdutoCodigo",
                table: "ProdutoPedido",
                column: "ProdutoCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoPedido");

            migrationBuilder.DropTable(
                name: "PedidosCompras");
        }
    }
}
