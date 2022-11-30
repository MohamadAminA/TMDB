using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.DataLayer.Migrations
{
    public partial class addSessionColforUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Session",
                table: "Users");
        }
    }
}
