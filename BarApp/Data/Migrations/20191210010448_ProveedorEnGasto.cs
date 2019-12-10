using Microsoft.EntityFrameworkCore.Migrations;

namespace BarApp.Data.Migrations
{
    public partial class ProveedorEnGasto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Gasto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_ProveedorId",
                table: "Gasto",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Proveedor_ProveedorId",
                table: "Gasto",
                column: "ProveedorId",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Proveedor_ProveedorId",
                table: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Gasto_ProveedorId",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Gasto");
        }
    }
}
