using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotServer.Data.Migrations
{
    public partial class updatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chatId",
                table: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParentId",
                table: "Messages",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ParentId",
                table: "Messages",
                column: "ParentId",
                principalTable: "Chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ParentId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ParentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "chatId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
