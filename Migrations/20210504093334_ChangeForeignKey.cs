using Microsoft.EntityFrameworkCore.Migrations;

namespace FunMath.Migrations
{
    public partial class ChangeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Levels_LevelId",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Challenges",
                newName: "LevelNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_LevelId",
                table: "Challenges",
                newName: "IX_Challenges_LevelNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Levels_LevelNumber",
                table: "Challenges",
                column: "LevelNumber",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Levels_LevelNumber",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "LevelNumber",
                table: "Challenges",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_LevelNumber",
                table: "Challenges",
                newName: "IX_Challenges_LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Levels_LevelId",
                table: "Challenges",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
