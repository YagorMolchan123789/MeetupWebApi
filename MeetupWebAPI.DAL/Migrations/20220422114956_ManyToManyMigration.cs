using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetupWebAPI.Migrations
{
    public partial class ManyToManyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetupUsers_Meetups_MeetupId",
                table: "MeetupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetupUsers_Users_UserId",
                table: "MeetupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetupUsers_MeetupId",
                table: "MeetupUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MeetupUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MeetupUsers",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "MeetupId",
                table: "MeetupUsers",
                newName: "MeetupsId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetupUsers_UserId",
                table: "MeetupUsers",
                newName: "IX_MeetupUsers_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers",
                columns: new[] { "MeetupsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupUsers_Meetups_MeetupsId",
                table: "MeetupUsers",
                column: "MeetupsId",
                principalTable: "Meetups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupUsers_Users_UsersId",
                table: "MeetupUsers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetupUsers_Meetups_MeetupsId",
                table: "MeetupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetupUsers_Users_UsersId",
                table: "MeetupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "MeetupUsers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "MeetupsId",
                table: "MeetupUsers",
                newName: "MeetupId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetupUsers_UsersId",
                table: "MeetupUsers",
                newName: "IX_MeetupUsers_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MeetupUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetupUsers",
                table: "MeetupUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetupUsers_MeetupId",
                table: "MeetupUsers",
                column: "MeetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupUsers_Meetups_MeetupId",
                table: "MeetupUsers",
                column: "MeetupId",
                principalTable: "Meetups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupUsers_Users_UserId",
                table: "MeetupUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
