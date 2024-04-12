using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessoManymindsBack.Migrations
{
    /// <inheritdoc />
    public partial class removeColumn_ValorUnitario_ProdutoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorUnitario",
                table: "ProdutoPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitario",
                table: "ProdutoPedido",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
