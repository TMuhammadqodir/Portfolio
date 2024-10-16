using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUploadTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUploadType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectUploadType",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "UserUploadType",
                table: "UserAssets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectUploadType",
                table: "ProjectAssets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUploadType",
                table: "UserAssets");

            migrationBuilder.DropColumn(
                name: "ProjectUploadType",
                table: "ProjectAssets");

            migrationBuilder.AddColumn<int>(
                name: "UserUploadType",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectUploadType",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
