using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Botserve.MigrationApp.Migrations
{
    public partial class addedSendToVkForuserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "sendToVk",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sendToVk",
                table: "AspNetUsers");
        }
    }
}
