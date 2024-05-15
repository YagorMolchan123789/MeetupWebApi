using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetupWebAPI.Migrations
{
    public partial class KeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MeetupUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetupUsers_UserId",
                table: "MeetupUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetupUsers_UserId",
                table: "MeetupUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MeetupUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers",
                columns: new[] { "UserId", "MeetupId" });
        }
    }
}
