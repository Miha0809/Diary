using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Services.Migrations
{
    public partial class RemovePathHomeworkInHomeworkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathHomework",
                table: "Homeworks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathHomework",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
