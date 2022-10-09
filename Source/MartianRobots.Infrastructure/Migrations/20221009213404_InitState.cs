using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartianRobots.Infrastructure.Migrations
{
    public partial class InitState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Robots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RobotMode = table.Column<int>(type: "int", nullable: false),
                    xPos = table.Column<int>(type: "int", nullable: true),
                    yPos = table.Column<int>(type: "int", nullable: true),
                    Orientation = table.Column<int>(type: "int", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    xPos = table.Column<int>(type: "int", nullable: false),
                    yPos = table.Column<int>(type: "int", nullable: false),
                    RobotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    MapId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cell_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cell_MapId",
                table: "Cell",
                column: "MapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cell");

            migrationBuilder.DropTable(
                name: "Robots");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
