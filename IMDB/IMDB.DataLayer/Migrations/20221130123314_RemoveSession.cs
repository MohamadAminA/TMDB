using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.DataLayer.Migrations
{
    public partial class RemoveSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Session",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
