using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManager.DataBase.SqlServer.Migrations
{
    public partial class AddingCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "varchar(150)", nullable: true),
                    CourseName = table.Column<string>(type: "varchar(150)", nullable: true),
                    TeacherName = table.Column<string>(type: "varchar(150)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "dateTime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "dateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
