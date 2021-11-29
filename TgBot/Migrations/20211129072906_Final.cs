using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "MyCurRoutes");

            migrationBuilder.AddColumn<bool>(
                name: "IsFromFirstStop",
                table: "MyCurRoutes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromFirstStop",
                table: "MyCurRoutes");

            migrationBuilder.AddColumn<bool>(
                name: "Direction",
                table: "MyCurRoutes",
                type: "bit",
                nullable: true);
        }
    }
}
