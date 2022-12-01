using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.DataLayer.Migrations
{
    public partial class replys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplayParent",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReplayParent",
                table: "Reviews",
                column: "ReplayParent");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reviews_ReplayParent",
                table: "Reviews",
                column: "ReplayParent",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reviews_ReplayParent",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReplayParent",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReplayParent",
                table: "Reviews");
        }
    }
}
