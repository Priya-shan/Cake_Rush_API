using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cake_Rush_API.Migrations
{
    public partial class M7_OrderModelColumnName_Altered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paymentMedium",
                table: "OrderModel",
                newName: "deliveryMode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deliveryMode",
                table: "OrderModel",
                newName: "paymentMedium");
        }
    }
}
