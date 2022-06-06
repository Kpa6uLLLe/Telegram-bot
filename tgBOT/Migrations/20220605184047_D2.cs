using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tgBOT.Migrations
{
    public partial class D2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Categories_CategoryId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_CategoryId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Links");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Links",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Links_CategoryName",
                table: "Links",
                column: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Categories_CategoryName",
                table: "Links",
                column: "CategoryName",
                principalTable: "Categories",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Categories_CategoryName",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_CategoryName",
                table: "Links");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Links");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Links",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Links_CategoryId",
                table: "Links",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Categories_CategoryId",
                table: "Links",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
