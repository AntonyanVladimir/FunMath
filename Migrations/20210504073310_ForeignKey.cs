using Microsoft.EntityFrameworkCore.Migrations;

namespace FunMath.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Levels_LevelId",
                table: "Challenges");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Levels_LevelId",
                table: "Challenges",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Levels_LevelId",
                table: "Challenges");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Challenges",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Levels_LevelId",
                table: "Challenges",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
