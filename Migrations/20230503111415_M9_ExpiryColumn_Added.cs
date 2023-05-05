using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cake_Rush_API.Migrations
{
    public partial class M9_ExpiryColumn_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "expiry",
                table: "CartModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "expiry",
                table: "CartModel");

           
        }
    }
}
