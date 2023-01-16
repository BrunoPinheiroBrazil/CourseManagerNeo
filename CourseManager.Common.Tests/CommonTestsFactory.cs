using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using System;
using System.Linq;

namespace CourseManager.Common.Tests
{
  public static class CommonTestsFactory
  {
    private static Random random = new Random();

    public static StudentDto CreateStudentDto(string gender, int randomLength, Student student = null)
    {
      if (student != null)
        return new StudentDto
        {
          StudentId = student.StudentId,
          FirstName = student.FirstName,
          SurName = student.SurName,
          Address1 = student.Address1,
          Address2 = student.Address2,
          Address3 = student.Address3,
          Dob = student.Dob,
          Gender = student.Gender
        };

      var randomString = RandomString(randomLength);
      return new StudentDto
      {
        FirstName = $"SomeFirstName {randomString}",
        SurName = $"SomeSurName {randomString}",
        Address1 = $"SomeAddres1{randomString}",
        Address2 = $"SomeAddres2{randomString}",
        Address3 = $"SomeAddres3{randomString}",
        Gender = gender,
        Dob = DateTime.Now.AddYears(-30)
      };
    }

    public static CourseDto CreateCourseDto()
    {
      var randomString = RandomString(4);

      return new CourseDto
      {
        CourseCode = $"CourseCode{randomString}",
        CourseName = $"CourseName{randomString}",
        TeacherName = $"TeacherName{randomString}",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now
      };
    }

    public static Student CreateStudent(string gender, int randomLength, StudentDto studentDto = null)
    {
      if (studentDto != null)
        return new Student
        {
          FirstName = studentDto.FirstName,
          SurName = studentDto.SurName,
          Address1 = studentDto.Address1,
          Address2 = studentDto.Address2,
          Address3 = studentDto.Address3,
          Dob = studentDto.Dob,
          Gender = studentDto.Gender
        };

      var randomString = RandomString(randomLength);
      return new Student
      {
        FirstName = $"SomeFirstName {randomString}",
        SurName = $"SomeSurName {randomString}",
        Address1 = $"SomeAddres1{randomString}",
        Address2 = $"SomeAddres2{randomString}",
        Address3 = $"SomeAddres3{randomString}",
        Gender = gender,
        Dob = DateTime.Now.AddYears(-30)
      };
    }

    public static Course CreateCourse(CourseDto courseDto = null)
    {
      if (courseDto != null)
        return new Course
        {
          CourseCode = courseDto.CourseCode,
          CourseName = courseDto.CourseName,
          TeacherName = courseDto.TeacherName,
          StartDate = courseDto.StartDate,
          EndDate = courseDto.EndDate
        };

      var randomString = RandomString(4);
      return new Course
      {
        CourseCode = $"CourseCode{randomString}",
        CourseName = $"CourseName{randomString}",
        TeacherName = $"TeacherName{randomString}",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(30)
      };
    }

    private static string RandomString(int length)
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
  }
}
