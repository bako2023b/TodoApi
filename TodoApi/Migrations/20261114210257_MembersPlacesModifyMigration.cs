using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class MembersPlacesModifyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Member_MemberId",
                table: "TodoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Place_PlaceId",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Members_MemberId",
                table: "TodoItems",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Places_PlaceId",
                table: "TodoItems",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Members_MemberId",
                table: "TodoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Places_PlaceId",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Member_MemberId",
                table: "TodoItems",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Place_PlaceId",
                table: "TodoItems",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id");
        }
    }
}
