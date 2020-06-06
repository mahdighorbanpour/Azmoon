using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class added_mathsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MatchSetId",
                table: "Choices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MatchSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchSet_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_MatchSetId",
                table: "Choices",
                column: "MatchSetId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchSet_QuestionId",
                table: "MatchSet",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_MatchSet_MatchSetId",
                table: "Choices",
                column: "MatchSetId",
                principalTable: "MatchSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_MatchSet_MatchSetId",
                table: "Choices");

            migrationBuilder.DropTable(
                name: "MatchSet");

            migrationBuilder.DropIndex(
                name: "IX_Choices_MatchSetId",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "MatchSetId",
                table: "Choices");
        }
    }
}
