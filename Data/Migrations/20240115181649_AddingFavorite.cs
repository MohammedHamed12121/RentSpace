using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSpace.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteId",
                table: "Spaces",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavoriteId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_FavoriteId",
                table: "Spaces",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavoriteId",
                table: "AspNetUsers",
                column: "FavoriteId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Favorite_FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Favorite_FavoriteId",
                table: "Spaces");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_FavoriteId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "AspNetUsers");
        }
    }
}
