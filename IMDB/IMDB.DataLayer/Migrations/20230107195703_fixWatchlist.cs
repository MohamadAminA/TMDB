using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.DataLayer.Migrations
{
    public partial class fixWatchlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists",
                column: "UserId",
                unique: true);
        }
    }
}
