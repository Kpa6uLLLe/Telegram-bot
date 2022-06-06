using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tgBOT.Migrations
{
    public partial class D1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Links_CategoryName",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Categories");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "CategoryName",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Links_CategoryName",
                table: "Categories",
                column: "CategoryName",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
