using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_numberOfIncoming",
                table: "MyCurRoutes",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_numberOfLeaving",
                table: "MyCurRoutes",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_timeOfStops",
                table: "MyCurRoutes",
                type: "varchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_numberOfIncoming",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "_numberOfLeaving",
                table: "MyCurRoutes");

            migrationBuilder.DropColumn(
                name: "_timeOfStops",
                table: "MyCurRoutes");
        }
    }
}
