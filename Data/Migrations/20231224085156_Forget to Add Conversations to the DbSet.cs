using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSpace.Data.Migrations
{
    /// <inheritdoc />
    public partial class ForgettoAddConversationstotheDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversions",
                table: "Conversions");

            migrationBuilder.RenameTable(
                name: "Conversions",
                newName: "Conversations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversions",
                table: "Conversions",
                column: "Id");
        }
    }
}
