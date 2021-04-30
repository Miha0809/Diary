using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Services.Migrations
{
    public partial class ChangeHomeworkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextToHomework",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextToHomework",
                table: "Homeworks");
        }
    }
}
