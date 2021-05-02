using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Services.Migrations
{
    public partial class AddModelCalendarAndChangeTeacherModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Lessons_LessonsId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "LessonsId",
                table: "Teachers",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_LessonsId",
                table: "Teachers",
                newName: "IX_Teachers_LessonId");

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_LessonId",
                table: "Calendars",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Lessons_LessonId",
                table: "Teachers",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Lessons_LessonId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Teachers",
                newName: "LessonsId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_LessonId",
                table: "Teachers",
                newName: "IX_Teachers_LessonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Lessons_LessonsId",
                table: "Teachers",
                column: "LessonsId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
