using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class WithoutBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromFirstStop",
                table: "MyCurRoutes");

            migrationBuilder.RenameColumn(
                name: "RecordID",
                table: "MyCurRoutes",
                newName: "RecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "MyCurRoutes",
                newName: "RecordID");

            migrationBuilder.AddColumn<bool>(
                name: "IsFromFirstStop",
                table: "MyCurRoutes",
                type: "bit",
                nullable: true);
        }
    }
}
