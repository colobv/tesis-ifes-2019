using Microsoft.EntityFrameworkCore.Migrations;

namespace BarApp.Data.Migrations
{
    public partial class PedidoPrecioTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioTotal",
                table: "Pedido",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioTotal",
                table: "Pedido");
        }
    }
}
