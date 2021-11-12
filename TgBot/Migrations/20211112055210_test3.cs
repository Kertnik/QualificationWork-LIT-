using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteRouteId",
                table: "MyDrivers");

            migrationBuilder.RenameColumn(
                name: "OrdinalRouteRouteId",
                table: "MyDrivers",
                newName: "OrdinalRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_MyDrivers_OrdinalRouteRouteId",
                table: "MyDrivers",
                newName: "IX_MyDrivers_OrdinalRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteId",
                table: "MyDrivers",
                column: "OrdinalRouteId",
                principalTable: "MyRoutes",
                principalColumn: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteId",
                table: "MyDrivers");

            migrationBuilder.RenameColumn(
                name: "OrdinalRouteId",
                table: "MyDrivers",
                newName: "OrdinalRouteRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_MyDrivers_OrdinalRouteId",
                table: "MyDrivers",
                newName: "IX_MyDrivers_OrdinalRouteRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteRouteId",
                table: "MyDrivers",
                column: "OrdinalRouteRouteId",
                principalTable: "MyRoutes",
                principalColumn: "RouteId");
        }
    }
}
