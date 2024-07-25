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
          CommonTestsFactory.CreateStudent(gender:"M", Id: 1),
          CommonTestsFactory.CreateStudent("F", Id: 2),
          CommonTestsFactory.CreateStudent("M", Id: 3),
          CommonTestsFactory.CreateStudent("F", Id: 4),
          CommonTestsFactory.CreateStudent("M", Id: 35403293)
      };
    }
    private static List<Course> GetCoursesList()
    {
      return new List<Course>()
      {
          CommonTestsFactory.CreateCourse(Id: 1),
          CommonTestsFactory.CreateCourse(Id: 2),
          CommonTestsFactory.CreateCourse(Id: 3),
          CommonTestsFactory.CreateCourse(Id: 4)
      };
    }
  }
}
