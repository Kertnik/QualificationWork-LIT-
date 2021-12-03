using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class cur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfStops",
                table: "MyRoutes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "NumberOfStops",
                table: "MyRoutes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
