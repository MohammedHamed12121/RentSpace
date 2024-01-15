using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSpace.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Favorite_FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Favorite_FavoriteId",
                table: "Spaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "Favorites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Favorites_FavoriteId",
                table: "AspNetUsers",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Favorites_FavoriteId",
                table: "Spaces",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Favorites_FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Favorites_FavoriteId",
                table: "Spaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Favorite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Favorite_FavoriteId",
                table: "AspNetUsers",
                column: "FavoriteId",
                principalTable: "Favorite",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Favorite_FavoriteId",
                table: "Spaces",
                column: "FavoriteId",
                principalTable: "Favorite",
                principalColumn: "Id");
        }
    }
}
