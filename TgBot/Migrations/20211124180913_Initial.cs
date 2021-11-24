using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_timeOfStops",
                table: "MyCurRoutes",
                newName: "TimeOfStops");

            migrationBuilder.RenameColumn(
                name: "_numberOfLeaving",
                table: "MyCurRoutes",
                newName: "NumberOfLeaving");

            migrationBuilder.RenameColumn(
                name: "_numberOfIncoming",
                table: "MyCurRoutes",
                newName: "NumberOfIncoming");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfStops",
                table: "MyCurRoutes",
                newName: "_timeOfStops");

            migrationBuilder.RenameColumn(
                name: "NumberOfLeaving",
                table: "MyCurRoutes",
                newName: "_numberOfLeaving");

            migrationBuilder.RenameColumn(
                name: "NumberOfIncoming",
                table: "MyCurRoutes",
                newName: "_numberOfIncoming");
        }
    }
}
