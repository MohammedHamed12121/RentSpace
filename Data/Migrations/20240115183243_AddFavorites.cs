using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSpace.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Favorites_FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Favorites_FavoriteId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_FavoriteId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Spaces");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpaceId",
                table: "Favorites",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FavoriteId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AppUserId",
                table: "Favorites",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_SpaceId",
                table: "Favorites",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_AppUserId",
                table: "Favorites",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Spaces_SpaceId",
                table: "Favorites",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_AppUserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Spaces_SpaceId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AppUserId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_SpaceId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "SpaceId",
                table: "Favorites");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteId",
                table: "Spaces",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FavoriteId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_FavoriteId",
                table: "Spaces",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavoriteId",
                table: "AspNetUsers",
                column: "FavoriteId");

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
    }
}
