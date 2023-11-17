using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderFood.Migrations
{
    public partial class CafesIdforDishModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CafeId",
                table: "Dishes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CafesId",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_CafeId",
                table: "Dishes",
                column: "CafeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Cafes_CafeId",
                table: "Dishes",
                column: "CafeId",
                principalTable: "Cafes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Cafes_CafeId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_CafeId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "CafeId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "CafesId",
                table: "Dishes");
        }
    }
}
