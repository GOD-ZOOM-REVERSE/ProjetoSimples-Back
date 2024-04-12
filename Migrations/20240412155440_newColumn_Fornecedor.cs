using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessoManymindsBack.Migrations
{
    /// <inheritdoc />
    public partial class newColumn_Fornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fornecedor",
                table: "PedidosCompras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fornecedor",
                table: "PedidosCompras");
        }
    }
}
