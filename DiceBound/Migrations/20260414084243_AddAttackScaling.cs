using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceBound.Migrations
{
    /// <inheritdoc />
    public partial class AddAttackScaling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttackBonus",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseAttack",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackBonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseAttack",
                table: "Characters");
        }
    }
}
