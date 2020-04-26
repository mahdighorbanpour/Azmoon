using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class fix_quizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Questions_QuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Quizzes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuestionId",
                table: "Quizzes",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Questions_QuestionId",
                table: "Quizzes",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
