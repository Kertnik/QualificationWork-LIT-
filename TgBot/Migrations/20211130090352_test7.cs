using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverId",
                table: "MyCurRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes");

            migrationBuilder.DropIndex(
                name: "IX_MyCurRoutes_DriverId",
                table: "MyCurRoutes");

            migrationBuilder.DropIndex(
                name: "IX_MyCurRoutes_RouteId",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "MyCurRoutes");

            migrationBuilder.AddColumn<string>(
                name: "DriverForeignKey",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteForeignKey",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_DriverForeignKey",
                table: "MyCurRoutes",
                column: "DriverForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_RouteForeignKey",
                table: "MyCurRoutes",
                column: "RouteForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverForeignKey",
                table: "MyCurRoutes",
                column: "DriverForeignKey",
                principalTable: "MyDrivers",
                principalColumn: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteForeignKey",
                table: "MyCurRoutes",
                column: "RouteForeignKey",
                principalTable: "MyRoutes",
                principalColumn: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyDrivers_DriverForeignKey",
                table: "MyCurRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteForeignKey",
                table: "MyCurRoutes");

            migrationBuilder.DropIndex(
                name: "IX_MyCurRoutes_DriverForeignKey",
                table: "MyCurRoutes");

            migrationBuilder.DropIndex(
                name: "IX_MyCurRoutes_RouteForeignKey",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "DriverForeignKey",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "RouteForeignKey",
                table: "MyCurRoutes");

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RouteId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_DriverId",
                table: "MyCurRoutes",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_RouteId",
                table: "MyCurRoutes",
                column: "RouteId");

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
    }
}
