using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskModified.Migrations
{
    public partial class school1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentEnrollment",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentEnrollment",
                table: "Students",
                column: "StudentEnrollment",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_StudentEnrollment",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "StudentEnrollment",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
