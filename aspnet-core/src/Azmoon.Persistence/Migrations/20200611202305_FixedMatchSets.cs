using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class FixedMatchSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_MatchSet_MatchSetId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchSet_Questions_QuestionId",
                table: "MatchSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchSet",
                table: "MatchSet");

            migrationBuilder.RenameTable(
                name: "MatchSet",
                newName: "MatchSets");

            migrationBuilder.RenameIndex(
                name: "IX_MatchSet_QuestionId",
                table: "MatchSets",
                newName: "IX_MatchSets_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchSets",
                table: "MatchSets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_MatchSets_MatchSetId",
                table: "Choices",
                column: "MatchSetId",
                principalTable: "MatchSets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSets_Questions_QuestionId",
                table: "MatchSets",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_MatchSets_MatchSetId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchSets_Questions_QuestionId",
                table: "MatchSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchSets",
                table: "MatchSets");

            migrationBuilder.RenameTable(
                name: "MatchSets",
                newName: "MatchSet");

            migrationBuilder.RenameIndex(
                name: "IX_MatchSets_QuestionId",
                table: "MatchSet",
                newName: "IX_MatchSet_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchSet",
                table: "MatchSet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_MatchSet_MatchSetId",
                table: "Choices",
                column: "MatchSetId",
                principalTable: "MatchSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSet_Questions_QuestionId",
                table: "MatchSet",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
