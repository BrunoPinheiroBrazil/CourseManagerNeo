using CourseManager.Common.Tests;
using CourseManager.Controllers;
using CourseManager.Models.Dtos;
using CourseManagerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CourseManager.Tests
{
  public class CourseManagerControllerTests
  {
    private readonly CourseManagerController _controller;
    private readonly Mock<IServices> _services;

    public CourseManagerControllerTests()
    {
      _services = new Mock<IServices>();
      _controller = new CourseManagerController(_services.Object);
    }

    #region Student
    [Fact(DisplayName = "GetStudent [Success]")]
    public async Task GetStudent_Success()
    {
      //Arrange
      var studentId = 3120L;
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 4);

      _services.Setup(s => s.GetStudent(studentId)).ReturnsAsync(studentDto);

      //Act
      var response = await _controller.GetStudent(studentId);

      //Assert
      var statusResult = Assert.IsType<OkObjectResult>(response);
      Assert.Equal(StatusCodes.Status200OK, statusResult.StatusCode);
      Assert.IsType<StudentDto>(statusResult.Value);
      _services.Verify(s => s.GetStudent(studentId), Times.Once, "GetStudent should be called once.");
    }

    [Fact(DisplayName = "SearchStudent [Success]")]
    public async Task SearchStudent_Success()
    {
      //Arrange
      var totalCount = 5;
      var searchTerms = new SearchTermsDto
      {
        FirstName = "SomeFirstName"
      };

      var paginatedResultsDto = new PaginatedResultsDto<StudentDto>
      {
        Page = 1,
        PageSize = 25,
        TotalCount = totalCount,
        Values = new List<StudentDto>
        {
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4)
        }
      };

      _services.Setup(s => s.SearchStudentsAsync(searchTerms, 25, 1)).ReturnsAsync((paginatedResultsDto.Values, totalCount));

      //Act
      var response = await _controller.SearchStudents(searchTerms, 25, 1);

      //Assert
      var statusResult = Assert.IsType<OkObjectResult>(response);
      Assert.Equal(StatusCodes.Status200OK, statusResult.StatusCode);
      Assert.IsType<PaginatedResultsDto<StudentDto>>(statusResult.Value);
      _services.Verify(s => s.SearchStudentsAsync(searchTerms, 25, 1), Times.Once, "SearchStudentsAsync should be called once.");
    }

    [Fact(DisplayName = "ListStudents [Success]")]
    public async Task ListStudents_Success()
    {
      //Arrange
      var totalCount = 5;

      var paginatedResultsDto = new PaginatedResultsDto<StudentDto>
      {
        Page = 1,
        PageSize = 25,
        TotalCount = totalCount,
        Values = new List<StudentDto>
        {
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4),
          CommonTestsFactory.CreateStudentDto("M", 4)
        }
      };

      _services.Setup(s => s.ListStudentsAsync(25, 1)).ReturnsAsync((paginatedResultsDto.Values, totalCount));

      //Act
      var response = await _controller.ListStudents(25, 1);

      //Assert
      var statusResult = Assert.IsType<OkObjectResult>(response);
      Assert.Equal(StatusCodes.Status200OK, statusResult.StatusCode);
      Assert.IsType<PaginatedResultsDto<StudentDto>>(statusResult.Value);
      _services.Verify(s => s.ListStudentsAsync(25, 1), Times.Once, "SearchStudentsAsync should be called once.");
    }

    [Fact(DisplayName = "Add Student [Success]")]
    public async Task InsertStudent_Success()
    {
      //Arrange
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 4);

      _services.Setup(s => s.InsertStudentAsync(studentDto)).ReturnsAsync(5);

      //Act
      var response = await _controller.InsertStudent(studentDto);

      //Assert
      var responseStatus = Assert.IsType<OkObjectResult>(response);
      Assert.Equal(200, responseStatus.StatusCode);
      _services.Verify(s => s.InsertStudentAsync(studentDto), Times.Once, "InsertStudent should be called once");
    }

    [Fact(DisplayName = "UpdateStudent [Success]")]
    public async Task UpdateStudent_Success()
    {
      //Arrange
      var studentId = 3L;
      var studentDto = CommonTestsFactory.CreateStudentDto("F", 4);

      _services.Setup(s => s.UpdateStudentAsync(studentId, studentDto));

      //Act
      var response = await _controller.UpdateStudent(studentId, studentDto);

      //Assert
      var responseStatus = Assert.IsType<NoContentResult>(response);
      Assert.Equal(204, responseStatus.StatusCode);
      _services.Verify(s => s.UpdateStudentAsync(studentId, studentDto), Times.Once, "UpdateStudent should be called once");
    }

    [Fact(DisplayName = "DeleteStudent [Success]")]
    public async Task DeleteStudent_Success()
    {
      //Arrange
      var studentId = 3L;

      //Act
      var response = await _controller.DeleteStudent(studentId);

      //Assert
      var responseStatus = Assert.IsType<NoContentResult>(response);
      Assert.Equal(204, responseStatus.StatusCode);
      _services.Verify(s => s.DeleteStudentAsync(studentId), Times.Once, "DeleteStudentAsync should be called once");
    }
    #endregion

    #region Course
    [Fact(DisplayName = "Add Course [Success]")]
    public async Task InsertCourse_Success()
    {
      //Arrange
      var courseDto = CommonTestsFactory.CreateCourseDto();

      _services.Setup(s => s.InsertCourseAsync(courseDto)).ReturnsAsync(5);

      //Act
      var response = await _controller.InsertCourse(courseDto);

      //Assert
      var responseStatus = Assert.IsType<OkObjectResult>(response);
      Assert.Equal(200, responseStatus.StatusCode);
      _services.Verify(s => s.InsertCourseAsync(courseDto), Times.Once, "InsertCourse should be called once");
    }

    [Fact(DisplayName = "UpdateCourse [Success]")]
    public async Task UpdateCourse_Success()
    {
      //Arrange
      var courseId = 2L;
      var courseDto = CommonTestsFactory.CreateCourseDto();

      _services.Setup(s => s.UpdateCourseAsync(courseId, courseDto));

      //Act
      var response = await _controller.UpdateCourse(courseId, courseDto);

      //Assert
      var responseStatus = Assert.IsType<NoContentResult>(response);
      Assert.Equal(204, responseStatus.StatusCode);
      _services.Verify(s => s.UpdateCourseAsync(courseId, courseDto), Times.Once, "UpdateCourse should be called once");
    }
    #endregion
  }
}
