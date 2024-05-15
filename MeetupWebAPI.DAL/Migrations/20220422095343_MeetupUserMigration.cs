using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupWebAPI.Migrations
{
    public partial class MeetupUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MeetupUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MeetupUsers");
        }
    }
}
