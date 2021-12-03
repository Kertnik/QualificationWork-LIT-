using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteRouteId",
                table: "MyDrivers");

            migrationBuilder.DropIndex(
                name: "IX_MyDrivers_OrdinalRouteRouteId",
                table: "MyDrivers");

            migrationBuilder.DropColumn(
                name: "OrdinalRouteRouteId",
                table: "MyDrivers");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFromFirstStop",
                table: "MyCurRoutes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrdinalRouteRouteId",
                table: "MyDrivers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFromFirstStop",
                table: "MyCurRoutes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyDrivers_OrdinalRouteRouteId",
                table: "MyDrivers",
                column: "OrdinalRouteRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteRouteId",
                table: "MyDrivers",
                column: "OrdinalRouteRouteId",
                principalTable: "MyRoutes",
                principalColumn: "RouteId");
        }
    }
}
