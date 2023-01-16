using CourseManager.Common.Tests;
using CourseManager.DataBase.SqlServer.DataAccess;
using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CourseManager.DataStore.SqlServer.Tests
{
  public class QueriesTests : IClassFixture<CourseManagerContextFixture>
  {
    private readonly CourseManagerContextFixture _fixture;
    private readonly IQueries _queries;

    public QueriesTests()
    {
      _fixture = new CourseManagerContextFixture();
      _queries = new Queries(_fixture.Context);
    }

    [Fact(DisplayName = "GetStudent [Success]")]
    public async Task GetStudent_Success()
    {
      //Arrange
      var student = CommonTestsFactory.CreateStudent("M", 4);

      await _fixture.Context.AddAsync(student);
      await _fixture.Context.SaveChangesAsync();

      var studentId = student.StudentId;

      //Act
      var currentStudent = await _queries.GetStudent(studentId);

      //Assert
      Assert.NotNull(currentStudent);
      Assert.Equal(student.FirstName, currentStudent.FirstName);
      Assert.Equal(student.SurName, currentStudent.SurName);
      Assert.Equal(student.Gender, currentStudent.Gender);
      Assert.Equal(student.Address1, currentStudent.Address1);
      Assert.Equal(student.Address2, currentStudent.Address2);
      Assert.Equal(student.Address3, currentStudent.Address3);
      Assert.Equal(student.Dob, currentStudent.Dob);
    }

    [Fact(DisplayName = "ListStudents [Success]")]
    public async Task ListStudents_Success()
    {
      //Arrange
      var students = new List<Student>
      {
        CommonTestsFactory.CreateStudent("M",4),
        CommonTestsFactory.CreateStudent("F",4),
        CommonTestsFactory.CreateStudent("M",4),
        CommonTestsFactory.CreateStudent("F",4)
      };

      students.ForEach(s =>
      {
        s.FirstName = "AThirdDifferentName";
      });

      await _fixture.Context.AddRangeAsync(students);
      await _fixture.Context.SaveChangesAsync();

      //Act
      var (currentStudents, totalCount) = await _queries.ListStudents(1, 4);

      //Assert
      Assert.Equal(4, totalCount);
      Assert.Equal(4, currentStudents.Count);
    }

    [Fact(DisplayName = "GetStudents [Success]")]
    public async Task GetStudents_Success()
    {
      //Arrange
      var students = new List<Student>
      {
        CommonTestsFactory.CreateStudent("F",4),
        CommonTestsFactory.CreateStudent("F",4),
        CommonTestsFactory.CreateStudent("F",4),
        CommonTestsFactory.CreateStudent("F",4)
      };

      students.ForEach(s =>
      {
        s.FirstName = "DifferentFirstName";
      });

      var searchTermsDto = new SearchTermsDto
      {
        FirstName = "DifferentFirstName"
      };

      await _fixture.Context.AddRangeAsync(students);
      await _fixture.Context.SaveChangesAsync();

      //Act
      var (currentStudents, totalCount) = await _queries.GetStudents(searchTermsDto);

      //Assert
      Assert.Equal(4, totalCount);
      Assert.Collection(currentStudents,
      s1 =>
      {
        Assert.Equal("DifferentFirstName", s1.FirstName);
      },
      s2 =>
      {
        Assert.Equal("DifferentFirstName", s2.FirstName);
      },
      s3 =>
      {
        Assert.Equal("DifferentFirstName", s3.FirstName);
      },
      s4 =>
      {
        Assert.Equal("DifferentFirstName", s4.FirstName);
      });
    }

    [Fact(DisplayName = "GetCourse [Success]")]
    public async Task GetCourse_Success()
    {
      //Arrange
      var course = CommonTestsFactory.CreateCourse();

      await _fixture.Context.AddAsync(course);
      await _fixture.Context.SaveChangesAsync();

      var courseId = course.CourseId;

      //Act
      var currentCourse = await _queries.GetCourse(courseId);

      //Assert
      Assert.NotNull(currentCourse);
      Assert.Equal(course.CourseCode, currentCourse.CourseCode);
      Assert.Equal(course.CourseName, currentCourse.CourseName);
      Assert.Equal(course.TeacherName, currentCourse.TeacherName);
      Assert.Equal(course.StartDate, currentCourse.StartDate);
      Assert.Equal(course.EndDate, currentCourse.EndDate);
    }
  }
}