using Microsoft.EntityFrameworkCore.Migrations;

namespace BarApp.Data.Migrations
{
    public partial class PedidoMesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mesa",
                table: "Pedido",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mesa",
                table: "Pedido");
        }
    }
}
