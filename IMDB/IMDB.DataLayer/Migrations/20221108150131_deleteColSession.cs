using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.DataLayer.Migrations
{
    public partial class deleteColSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireSession",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GuestSessionId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireSession",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GuestSessionId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
