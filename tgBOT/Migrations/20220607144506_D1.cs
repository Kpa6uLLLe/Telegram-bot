using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tgBOT.Migrations
{
    public partial class D1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUsers",
                newName: "BotAPIUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BotAPIUserId",
                table: "AspNetUsers",
                newName: "UserId");
        }
    }
}
