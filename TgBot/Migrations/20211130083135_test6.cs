using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes");

            migrationBuilder.AlterColumn<string>(
                name: "RouteId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes",
                column: "DriverId",
                principalTable: "MyDrivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes",
                column: "RouteId",
                principalTable: "MyRoutes",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes");

            migrationBuilder.AlterColumn<string>(
                name: "RouteId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes",
                column: "RouteId",
                principalTable: "MyRoutes",
                principalColumn: "RouteId");
        }
    }
}
