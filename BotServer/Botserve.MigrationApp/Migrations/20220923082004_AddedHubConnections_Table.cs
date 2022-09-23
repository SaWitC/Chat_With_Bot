using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Botserve.MigrationApp.Migrations
{
    public partial class AddedHubConnections_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sendToVk",
                table: "AspNetUsers",
                newName: "SendToVk");

            migrationBuilder.CreateTable(
                name: "HubConnections",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvtorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HubConnection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubConnections", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HubConnections");

            migrationBuilder.RenameColumn(
                name: "SendToVk",
                table: "AspNetUsers",
                newName: "sendToVk");
        }
    }
}
