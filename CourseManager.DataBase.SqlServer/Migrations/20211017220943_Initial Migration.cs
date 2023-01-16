using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManager.DataBase.SqlServer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    SurName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(2)", nullable: true),
                    Dob = table.Column<DateTime>(type: "dateTime", nullable: false),
                    Address1 = table.Column<string>(type: "varchar(150)", nullable: true),
                    Address2 = table.Column<string>(type: "varchar(150)", nullable: true),
                    Address3 = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
