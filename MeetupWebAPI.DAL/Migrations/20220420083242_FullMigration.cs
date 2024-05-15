using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupWebAPI.Migrations
{
    public partial class FullMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "text",
                nullable: false,
                computedColumnSql: "\"FirstName\" || ' ' || \"LastName\"",
                stored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");
        }
    }
}
