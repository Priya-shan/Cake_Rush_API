using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cake_Rush_API.Migrations
{
    public partial class M6_OrderModel_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderModel",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    orderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOrdered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentMedium = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModel", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_OrderModel_CartModel_cartId",
                        column: x => x.cartId,
                        principalTable: "CartModel",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderModel_cartId",
                table: "OrderModel",
                column: "cartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderModel");
        }
    }
}
