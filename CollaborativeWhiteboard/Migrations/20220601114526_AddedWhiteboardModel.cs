using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollaborativeWhiteboard.Migrations
{
    public partial class AddedWhiteboardModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWhiteBoard",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhiteboardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWhiteBoard", x => new { x.UserId, x.WhiteboardId });
                });

            migrationBuilder.CreateTable(
                name: "Whiteboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Canvas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whiteboard", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWhiteBoard");

            migrationBuilder.DropTable(
                name: "Whiteboard");
        }
    }
}
