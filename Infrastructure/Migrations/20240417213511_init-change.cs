using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseIds",
                table: "SavedCourses");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "SavedCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "SavedCourses");

            migrationBuilder.AddColumn<string>(
                name: "CourseIds",
                table: "SavedCourses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
