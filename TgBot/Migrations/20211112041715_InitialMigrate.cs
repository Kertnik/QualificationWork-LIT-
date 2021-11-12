using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyRoutes",
                columns: table => new
                {
                    RouteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Stops = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfStops = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRoutes", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "MyDrivers",
                columns: table => new
                {
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    OrdinalRouteRouteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyDrivers", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_MyDrivers_MyRoutes_OrdinalRouteRouteId",
                        column: x => x.OrdinalRouteRouteId,
                        principalTable: "MyRoutes",
                        principalColumn: "RouteId");
                });

            migrationBuilder.CreateTable(
                name: "MyCurRoutes",
                columns: table => new
                {
                    RecordID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PathId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCurRoutes", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_MyCurRoutes_MyDrivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "MyDrivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_DriverId",
                table: "MyCurRoutes",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_MyDrivers_OrdinalRouteRouteId",
                table: "MyDrivers",
                column: "OrdinalRouteRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyCurRoutes");

            migrationBuilder.DropTable(
                name: "MyDrivers");

            migrationBuilder.DropTable(
                name: "MyRoutes");
        }
    }
}
