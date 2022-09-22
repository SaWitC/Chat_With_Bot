using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Botserve.MigrationApp.Migrations
{
    public partial class AddedVkIdPropertieForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VkId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VkId",
                table: "AspNetUsers");
        }
    }
}
