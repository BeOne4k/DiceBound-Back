using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceBound.Migrations
{
    /// <inheritdoc />
    public partial class AddEnemy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BossId",
                table: "Missions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XpValue",
                table: "Bosses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    ArmorClass = table.Column<int>(type: "int", nullable: false),
                    XpValue = table.Column<int>(type: "int", nullable: false),
                    MissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enemies_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_BossId",
                table: "Missions",
                column: "BossId");

            migrationBuilder.CreateIndex(
                name: "IX_Enemies_MissionId",
                table: "Enemies",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Bosses_BossId",
                table: "Missions",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Bosses_BossId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropIndex(
                name: "IX_Missions_BossId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "XpValue",
                table: "Bosses");
        }
    }
}
