using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarApp.Data.Migrations
{
    public partial class CategoriaGasto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaGastoId",
                table: "Gasto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriaGasto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaGasto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_CategoriaGastoId",
                table: "Gasto",
                column: "CategoriaGastoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_CategoriaGasto_CategoriaGastoId",
                table: "Gasto",
                column: "CategoriaGastoId",
                principalTable: "CategoriaGasto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_CategoriaGasto_CategoriaGastoId",
                table: "Gasto");

            migrationBuilder.DropTable(
                name: "CategoriaGasto");

            migrationBuilder.DropIndex(
                name: "IX_Gasto_CategoriaGastoId",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "CategoriaGastoId",
                table: "Gasto");
        }
    }
}
