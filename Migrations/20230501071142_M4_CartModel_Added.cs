using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cake_Rush_API.Migrations
{
    public partial class M4_CartModel_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartModel",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mapId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartModel", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_CartModel_SubCategoryMapModel_mapId",
                        column: x => x.mapId,
                        principalTable: "SubCategoryMapModel",
                        principalColumn: "mapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartModel_UserModel_userId",
                        column: x => x.userId,
                        principalTable: "UserModel",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartModel_mapId",
                table: "CartModel",
                column: "mapId");

            migrationBuilder.CreateIndex(
                name: "IX_CartModel_userId",
                table: "CartModel",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartModel");
        }
    }
}
