using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OrderFood.Migrations
{
    public partial class AddedCartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DishId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_CafesId",
                table: "Dishes",
                column: "CafesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Cafes_CafesId",
                table: "Dishes",
                column: "CafesId",
                principalTable: "Cafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Cafes_CafesId",
                table: "Dishes");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_CafesId",
                table: "Dishes");

            migrationBuilder.AddColumn<int>(
                name: "CafeId",
                table: "Dishes",
                type: "integer",
                nullable: true);

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
    }
}
