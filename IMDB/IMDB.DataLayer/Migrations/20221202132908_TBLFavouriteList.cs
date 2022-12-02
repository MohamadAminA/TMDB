using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.DataLayer.Migrations
{
    public partial class TBLFavouriteList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteList_Users_UserId",
                table: "FavouriteList");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteMovie_FavouriteList_FavouriteListId",
                table: "FavouriteMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteMovie",
                table: "FavouriteMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteList",
                table: "FavouriteList");

            migrationBuilder.RenameTable(
                name: "FavouriteMovie",
                newName: "FavouriteMovies");

            migrationBuilder.RenameTable(
                name: "FavouriteList",
                newName: "FavouriteLists");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteMovie_FavouriteListId",
                table: "FavouriteMovies",
                newName: "IX_FavouriteMovies_FavouriteListId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteList_UserId",
                table: "FavouriteLists",
                newName: "IX_FavouriteLists_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteMovies",
                table: "FavouriteMovies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteLists",
                table: "FavouriteLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteLists_Users_UserId",
                table: "FavouriteLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteMovies_FavouriteLists_FavouriteListId",
                table: "FavouriteMovies",
                column: "FavouriteListId",
                principalTable: "FavouriteLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteLists_Users_UserId",
                table: "FavouriteLists");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteMovies_FavouriteLists_FavouriteListId",
                table: "FavouriteMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteMovies",
                table: "FavouriteMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteLists",
                table: "FavouriteLists");

            migrationBuilder.RenameTable(
                name: "FavouriteMovies",
                newName: "FavouriteMovie");

            migrationBuilder.RenameTable(
                name: "FavouriteLists",
                newName: "FavouriteList");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteMovies_FavouriteListId",
                table: "FavouriteMovie",
                newName: "IX_FavouriteMovie_FavouriteListId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteLists_UserId",
                table: "FavouriteList",
                newName: "IX_FavouriteList_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteMovie",
                table: "FavouriteMovie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteList",
                table: "FavouriteList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteList_Users_UserId",
                table: "FavouriteList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteMovie_FavouriteList_FavouriteListId",
                table: "FavouriteMovie",
                column: "FavouriteListId",
                principalTable: "FavouriteList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
