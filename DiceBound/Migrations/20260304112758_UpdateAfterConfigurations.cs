using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceBound.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAfterConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatLogs_Bosses_BossId",
                table: "CombatLogs");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatLogs_Bosses_BossId",
                table: "CombatLogs",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatLogs_Bosses_BossId",
                table: "CombatLogs");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatLogs_Bosses_BossId",
                table: "CombatLogs",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
