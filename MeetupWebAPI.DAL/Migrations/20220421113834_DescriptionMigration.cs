using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupWebAPI.Migrations
{
    public partial class DescriptionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Meetups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "Meetups",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "Meetups");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Meetups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
