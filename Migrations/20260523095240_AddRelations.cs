using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonCode",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Examinations");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Examinations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Examinations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_LessonId",
                table: "Examinations",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_StudentId",
                table: "Examinations",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Lessons_LessonId",
                table: "Examinations",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Students_StudentId",
                table: "Examinations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Lessons_LessonId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Students_StudentId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_LessonId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_StudentId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Examinations");

            migrationBuilder.AddColumn<string>(
                name: "LessonCode",
                table: "Examinations",
                type: "char(3)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Examinations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
