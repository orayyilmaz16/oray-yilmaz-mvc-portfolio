using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrayPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_VolunteerWorks_VolunteerWorkId",
                table: "MediaFiles");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "VolunteerWorks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_VolunteerWorks_VolunteerWorkId",
                table: "MediaFiles",
                column: "VolunteerWorkId",
                principalTable: "VolunteerWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_VolunteerWorks_VolunteerWorkId",
                table: "MediaFiles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "VolunteerWorks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Educations");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_VolunteerWorks_VolunteerWorkId",
                table: "MediaFiles",
                column: "VolunteerWorkId",
                principalTable: "VolunteerWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
