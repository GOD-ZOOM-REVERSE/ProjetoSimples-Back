using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessoManymindsBack.Migrations
{
    /// <inheritdoc />
    public partial class newColumn_CreatedAt_And_UpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PedidosCompras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PedidosCompras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PedidosCompras");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PedidosCompras");
        }
    }
}
