using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cake_Rush_API.Migrations
{
    public partial class M5_QuantitPriceColumn_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "CartModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "CartModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "CartModel");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "CartModel");
        }
    }
}
