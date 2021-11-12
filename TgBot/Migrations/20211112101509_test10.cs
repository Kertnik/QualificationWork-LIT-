using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrdinalRouteId",
                table: "MyDrivers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyDrivers_OrdinalRouteId",
                table: "MyDrivers",
                column: "OrdinalRouteId");

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

            migrationBuilder.DropIndex(
                name: "IX_MyDrivers_OrdinalRouteId",
                table: "MyDrivers");

            migrationBuilder.DropColumn(
                name: "OrdinalRouteId",
                table: "MyDrivers");
        }
    }
}
