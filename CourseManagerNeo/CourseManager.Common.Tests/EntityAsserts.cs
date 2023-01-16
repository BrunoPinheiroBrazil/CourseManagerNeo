using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CourseManager.Common.Tests
{
  public static class EntityAsserts
  {
    public async static Task AssertStudentAsync(StudentDto expectedStudentDto, Student currentStudent)
    {
      var tasks = new List<Task>();

      tasks.AddRange(new List<Task>
      {
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.FirstName, currentStudent.FirstName);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.SurName, currentStudent.SurName);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.Dob, currentStudent.Dob);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.Gender, currentStudent.Gender);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.Address1, currentStudent.Address1);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.Address2, currentStudent.Address2);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedStudentDto.Address3, currentStudent.Address3);
        }),
      });
      Task t = Task.WhenAll(tasks);

      await t;
    }

    public async static Task AssertCoursesAsync(CourseDto expectedCourseDto, Course currendCourse)
    {
      var tasks = new List<Task>();

      tasks.AddRange(new List<Task>
      {
        Task.Run(() =>
        {
          Assert.Equal(expectedCourseDto.CourseCode, currendCourse.CourseCode);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedCourseDto.CourseName, currendCourse.CourseName);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedCourseDto.TeacherName, currendCourse.TeacherName);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedCourseDto.StartDate, currendCourse.StartDate);
        }),
        Task.Run(() =>
        {
          Assert.Equal(expectedCourseDto.EndDate, currendCourse.EndDate);
        })
      });
      Task t = Task.WhenAll(tasks);

      await t;
    }
  }
}
