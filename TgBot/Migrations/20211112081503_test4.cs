using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "MyCurRoutes");

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes",
                column: "DriverId",
                principalTable: "MyDrivers",
                principalColumn: "DriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes");

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteId",
                table: "MyCurRoutes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes",
                column: "DriverId",
                principalTable: "MyDrivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
