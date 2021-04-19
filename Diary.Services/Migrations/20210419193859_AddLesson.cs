using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Services.Migrations
{
    public partial class AddLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "Homeworks");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Homeworks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_LessonId",
                table: "Homeworks",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Lesson_LessonId",
                table: "Homeworks",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Lesson_LessonId",
                table: "Homeworks");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_LessonId",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Homeworks");

            migrationBuilder.AddColumn<string>(
                name: "Lesson",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
