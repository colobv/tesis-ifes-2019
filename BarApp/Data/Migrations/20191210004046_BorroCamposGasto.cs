using Microsoft.EntityFrameworkCore.Migrations;

namespace BarApp.Data.Migrations
{
    public partial class BorroCamposGasto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                table: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Gasto_CategoriaId",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "Proveedor",
                table: "Gasto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Gasto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Proveedor",
                table: "Gasto",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_CategoriaId",
                table: "Gasto",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                table: "Gasto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
