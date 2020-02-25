using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarManagement.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    szEmail = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    szFullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    dtmCreated = table.Column<DateTime>(nullable: false),
                    dtmUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.szEmail);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
