using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessoManymindsBack.Migrations
{
    /// <inheritdoc />
    public partial class newColumn_Observacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "PedidosCompras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "PedidosCompras");
        }
    }
}
