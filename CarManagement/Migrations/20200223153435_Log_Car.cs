using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarManagement.Migrations
{
    public partial class Log_Car : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    szCarId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    szCarName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    szCarModel = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    dtmCreated = table.Column<DateTime>(nullable: false),
                    dtmUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.szCarId);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    gdHistoryId = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    szUri = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    szEmail = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    szAction = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    szData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtmCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.gdHistoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}
