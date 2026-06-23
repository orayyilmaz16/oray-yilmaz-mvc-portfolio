using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrayPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkedinUrl",
                table: "References",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "References",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedinUrl",
                table: "References");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "References");
        }
    }
}
