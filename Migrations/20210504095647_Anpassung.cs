using Microsoft.EntityFrameworkCore.Migrations;

namespace FunMath.Migrations
{
    public partial class Anpassung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Levels_LevelNumber",
                table: "Challenges");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_LevelNumber",
                table: "Challenges");

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_LevelId",
                table: "Challenges",
                column: "LevelId");

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

            migrationBuilder.DropIndex(
                name: "IX_Challenges_LevelId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Challenges");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_LevelNumber",
                table: "Challenges",
                column: "LevelNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Levels_LevelNumber",
                table: "Challenges",
                column: "LevelNumber",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
