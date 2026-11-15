using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaceAndMemberMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "TodoItems");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "TodoItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TodoItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "TodoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_MemberId",
                table: "TodoItems",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_PlaceId",
                table: "TodoItems",
                column: "PlaceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Member_MemberId",
                table: "TodoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Place_PlaceId",
                table: "TodoItems");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_MemberId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_PlaceId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "TodoItems");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "TodoItems",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
