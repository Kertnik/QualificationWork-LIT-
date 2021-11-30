using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgBot.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { migrationBuilder.DropTable(
                name: "MyCurRoutes");

            migrationBuilder.DropTable(
                name: "MyDrivers");

            migrationBuilder.DropTable(
                name: "MyRoutes");
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
                    TimeOfStops = table.Column<string>(type: "varchar(max)", nullable: true),
                    IsFromFirstStop = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfLeaving = table.Column<string>(type: "varchar(256)", nullable: true),
                    NumberOfIncoming = table.Column<string>(type: "varchar(256)", nullable: true),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RouteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCurRoutes", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_MyCurRoutes_MyDrivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "MyDrivers",
                        principalColumn: "DriverId");
                    table.ForeignKey(
                        name: "FK_MyCurRoutes_MyRoutes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "MyRoutes",
                        principalColumn: "RouteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_DriverId",
                table: "MyCurRoutes",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCurRoutes_RouteId",
                table: "MyCurRoutes",
                column: "RouteId");

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
