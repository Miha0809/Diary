using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Services.Migrations
{
    public partial class ChangeReadyHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDateTime",
                table: "ReadyHomeworks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDateTime",
                table: "ReadyHomeworks");
        }
    }
}
