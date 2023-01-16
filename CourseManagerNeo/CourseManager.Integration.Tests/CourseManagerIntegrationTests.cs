using CourseManager.Common.Tests;
using CourseManager.Models.Dtos;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CourseManager.Integration.Tests
{
  public class CourseManagerIntegrationTests : IClassFixture<CourseManagerFixture>
  {
    private readonly CourseManagerFixture _fixture;

    public CourseManagerIntegrationTests(CourseManagerFixture fixture)
    {
      _fixture = fixture;
    }

    #region Student
    [Fact(DisplayName = "Get Student [Success]")]
    public async Task Get_Student_Success()
    {
      //Arrange
      var studentId = 1;
      var url = $"coursemanager/student/{studentId}";

      //Act
      var (responseObject, StatusCode) = await _fixture.GetInApi<StudentDto>(url);

      //Assert
      Assert.Equal(HttpStatusCode.OK, StatusCode);
      Assert.IsType<StudentDto>(responseObject);
      Assert.NotNull(responseObject);
    }

    [Fact(DisplayName = "Search Students [Success]")]
    public async Task Search_Students_Success()
    {
      //Arrange
      var url = $"coursemanager/student";
      var searchTermsDto = new SearchTermsDto
      {
        FirstName = "SomeFirstName"
      };

      var searchTermsDtoJson = JToken.FromObject(searchTermsDto).ToString();

      //Act
      var (responseObject, StatusCode) = await _fixture.PostInApi<PaginatedResultsDto<StudentDto>>(url, searchTermsDtoJson);

      //Assert
      Assert.Equal(HttpStatusCode.OK, StatusCode);
      var results = Assert.IsType<PaginatedResultsDto<StudentDto>>(responseObject);
      Assert.True(results.Values.Any());
    }

    [Fact(DisplayName = "List Students [Success]")]
    public async Task List_Students_Success()
    {
      //Arrange
      var url = $"coursemanager/liststudents";

      //Act
      var (responseObject, StatusCode) = await _fixture.GetInApi<PaginatedResultsDto<StudentDto>>(url);

      //Assert
      Assert.Equal(HttpStatusCode.OK, StatusCode);
      var results = Assert.IsType<PaginatedResultsDto<StudentDto>>(responseObject);
      Assert.True(results.Values.Any());
    }

    [Fact(DisplayName = "Insert Student [Success]")]
    public async Task Insert_Student_Success()
    {
      //Arrange
      var url = "coursemanager/create/student";
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 4);

      var studentDtoJson = JToken.FromObject(studentDto).ToString();

      //Act
      var statusCode = await _fixture.PostInApi(url, studentDtoJson);

      //Assert
      Assert.Equal((int)HttpStatusCode.OK, statusCode);
    }

    [Fact(DisplayName = "Update Student [Success]")]
    public async Task Update_Student_Success()
    {
      //Arrange
      var studentId = 1;
      var url = $"coursemanager/update/student/{studentId}";
      var studentDto = CommonTestsFactory.CreateStudentDto("F", 4);
      var studentDtoJson = JToken.FromObject(studentDto).ToString();

      //Act
      var statusCode = await _fixture.PutInApi(url, studentDtoJson);

      //Assert
      Assert.Equal(HttpStatusCode.NoContent, statusCode);
    }

    [Fact(DisplayName = "Update Student [Failure] - Student does not exists")]
    public async Task Update_Student_Failure_Student_Does_Not_Exists()
    {
      //Arrange
      var studentId = 4343;
      var url = $"coursemanager/update/student/{studentId}";
      var studentDto = CommonTestsFactory.CreateStudentDto("F", 4);

      var studentDtoJson = JToken.FromObject(studentDto).ToString();

      //Act
      //Assert
      _ = await Assert.ThrowsAsync<InvalidOperationException>(() => _fixture.PutInApi(url, studentDtoJson));
    }

    [Fact(DisplayName = "Delete Student [Success]")]
    public async Task Delete_Student_Success()
    {
      //Arrange
      var studentId = 3;
      var url = $"coursemanager/delete/student/{studentId}";

      //Act
      var statusCode = await _fixture.DeleteInApi(url);

      //Assert
      Assert.Equal(HttpStatusCode.NoContent, statusCode);
    }
    #endregion

    #region Course
    [Fact(DisplayName = "Insert Course [Success]")]
    public async Task Insert_Course_Success()
    {
      //Arrange
      var url = "coursemanager/create/course";
      var courseDto = new CourseDto
      {
        CourseCode = "AB5ED",
        CourseName = "Magic",
        TeacherName = "Dumbledore",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddMonths(1)
      };

      var courseDtoJson = JToken.FromObject(courseDto).ToString();

      //Act
      var statusCode = await _fixture.PostInApi(url, courseDtoJson);

      //Assert
      Assert.Equal((int)HttpStatusCode.OK, statusCode);
    }

    [Fact(DisplayName = "Update Course [Success]")]
    public async Task Update_Course_Success()
    {
      //Arrange
      var courseId = 1;
      var url = $"coursemanager/update/course/{courseId}";
      var courseDto = CommonTestsFactory.CreateCourseDto();
      var courseDtoJson = JToken.FromObject(courseDto).ToString();

      //Act
      var statusCode = await _fixture.PutInApi(url, courseDtoJson);

      //Assert
      Assert.Equal(HttpStatusCode.NoContent, statusCode);
    }

    [Fact(DisplayName = "Update Course [Failure] - Course does not exists")]
    public async Task Update_Course_Failure_Student_Does_Not_Exists()
    {
      //Arrange
      var courseId = 434354;
      var url = $"coursemanager/update/course/{courseId}";
      var courseDto = CommonTestsFactory.CreateCourseDto();

      var courseDtoJson = JToken.FromObject(courseDto).ToString();

      //Act
      //Assert
      _ = await Assert.ThrowsAsync<InvalidOperationException>(() => _fixture.PutInApi(url, courseDtoJson));
    }
    #endregion
  }
}
