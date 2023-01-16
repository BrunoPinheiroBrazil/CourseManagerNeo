using CourseManager.Common.Tests;
using CourseManager.DataBase.SqlServer;
using CourseManager.Models.Entities;
using System.Collections.Generic;

namespace CourseManager.Integration.Tests
{
  public static class Utilities
  {
    public static void InitializeDbForTests(CourseManagerDbContext db)
    {
      db.Students.AddRange(GetStudentsList());
      db.Courses.AddRange(GetCoursesList());
      db.SaveChanges();
    }

    public static void ReinitializeDbForTests(CourseManagerDbContext db)
    {
      db.Students.RemoveRange(db.Students);
      db.Courses.RemoveRange(db.Courses);
      InitializeDbForTests(db);
    }

    private static List<Student> GetStudentsList()
    {
      return new List<Student>()
      {
          CommonTestsFactory.CreateStudent("M", 4),
          CommonTestsFactory.CreateStudent("F", 4),
          CommonTestsFactory.CreateStudent("M", 4),
          CommonTestsFactory.CreateStudent("F", 4)
      };
    }
    private static List<Course> GetCoursesList()
    {
      return new List<Course>()
      {
          CommonTestsFactory.CreateCourse(),
          CommonTestsFactory.CreateCourse(),
          CommonTestsFactory.CreateCourse(),
          CommonTestsFactory.CreateCourse()
      };
    }
  }
}
