using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyDrivers_MyRoutes_OrdinalRouteId",
                table: "MyDrivers");

            migrationBuilder.DropIndex(
                name: "IX_MyDrivers_OrdinalRouteId",
                table: "MyDrivers");

            migrationBuilder.AlterColumn<string>(
                name: "OrdinalRouteId",
                table: "MyDrivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrdinalRouteId",
                table: "MyDrivers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
