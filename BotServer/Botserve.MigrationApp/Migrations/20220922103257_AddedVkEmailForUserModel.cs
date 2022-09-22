using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Botserve.MigrationApp.Migrations
{
    public partial class AddedVkEmailForUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VkEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VkEmail",
                table: "AspNetUsers");
        }
    }
}
