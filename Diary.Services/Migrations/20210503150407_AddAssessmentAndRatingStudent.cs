using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Services.Migrations
{
    public partial class AddAssessmentAndRatingStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Assessment",
                table: "ReadyHomeworks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Assessment",
                table: "ReadyHomeworks");
        }
    }
}
