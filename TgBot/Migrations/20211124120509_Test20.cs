using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class Test20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouteId",
                table: "MyCurRoutes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_RouteId",
                table: "MyCurRoutes",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes",
                column: "RouteId",
                principalTable: "MyRoutes",
                principalColumn: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyCurRoutes_MyRoutes_RouteId",
                table: "MyCurRoutes");

            migrationBuilder.DropIndex(
                name: "IX_MyCurRoutes_RouteId",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "MyCurRoutes");
        }
    }
}
