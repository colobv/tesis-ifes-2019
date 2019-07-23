using Microsoft.EntityFrameworkCore.Migrations;

namespace BarApp.Data.Migrations
{
    public partial class CantidadPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "PedidoItem",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "PedidoItem");
        }
    }
}
