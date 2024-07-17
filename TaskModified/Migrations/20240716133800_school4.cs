using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskModified.Migrations
{
    public partial class school4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseID",
                table: "Grades");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseID",
                table: "Grades",
                column: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseID",
                table: "Grades");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseID",
                table: "Grades",
                column: "CourseID",
                unique: true);
        }
    }
}
