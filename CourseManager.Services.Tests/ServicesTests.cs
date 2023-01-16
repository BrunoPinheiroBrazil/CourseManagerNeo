using CourseManager.Common.Tests;
using CourseManager.DataBase.SqlServer.DataAccess;
using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using CourseManager.Models.Translators;
using CourseManagerServices;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CourseManagerServicesTests
{
  public class ServicesTests
  {
    private readonly Services _services;
    private readonly Mock<IQueries> _queries;
    private readonly Mock<ICommands> _commands;
    private readonly Mock<IToEntityTranslator> _toEntityTranslator;
    private readonly Mock<IToDtoTranslator> _toDtoTranslator;

    public ServicesTests()
    {
      _toEntityTranslator = new Mock<IToEntityTranslator>();
      _toDtoTranslator = new Mock<IToDtoTranslator>();
      _queries = new Mock<IQueries>();
      _commands = new Mock<ICommands>();
      _services = new Services(_toEntityTranslator.Object, _toDtoTranslator.Object, _commands.Object, _queries.Object);
    }

    #region Student
    [Fact(DisplayName = "GetStudent [Success]")]
    public async Task GetStudent_Success()
    {
      //Arrange
      var studentId = 40303L;
      var currentDbStudent = CommonTestsFactory.CreateStudent("M", 4);
      var translatedStudentDto = CommonTestsFactory.CreateStudentDto("M", 4, currentDbStudent);

      _queries.Setup(q => q.GetStudent(studentId)).ReturnsAsync(currentDbStudent);
      _toDtoTranslator.Setup(t => t.ToStudentDto(currentDbStudent)).ReturnsAsync(translatedStudentDto);

      //Act
      var currentStudent = await _services.GetStudent(studentId);

      //Assert
      Assert.NotNull(currentStudent);
      _queries.Verify(q => q.GetStudent(studentId), Times.Once, "GetStudent should be called once");
      _toDtoTranslator.Verify(t => t.ToStudentDto(currentDbStudent), Times.Once, "ToStudentDto should be called once");
    }

    [Fact(DisplayName = "SearchStudents [Success]")]
    public async Task SearchStudent_Success()
    {
      //Arrange
      var page = 1;
      var pageSize = 25;
      var searchTermsDto = new SearchTermsDto
      {
        FirstName = "SomeFirstName"
      };

      var students = new List<Student>
      {
        CommonTestsFactory.CreateStudent("M", 4),
        CommonTestsFactory.CreateStudent("F", 4),
        CommonTestsFactory.CreateStudent("F", 4),
        CommonTestsFactory.CreateStudent("F", 4),
        CommonTestsFactory.CreateStudent("F", 4),
      };

      var studentsDto = new List<StudentDto>
      {
        CommonTestsFactory.CreateStudentDto("M", 4, students[0]),
        CommonTestsFactory.CreateStudentDto("M", 4, students[1]),
        CommonTestsFactory.CreateStudentDto("F", 4, students[2]),
        CommonTestsFactory.CreateStudentDto("F", 4, students[3]),
        CommonTestsFactory.CreateStudentDto("M", 4, students[4])
      };

      _queries.Setup(q => q.GetStudents(searchTermsDto, 1, 25)).ReturnsAsync((students, 5));
      _toDtoTranslator.Setup(t => t.ToStudentsDto(students)).ReturnsAsync(studentsDto);

      //Act
      var (content, totalCound) = await _services.SearchStudentsAsync(searchTermsDto, pageSize, page);

      //Assert
      Assert.Equal(5, totalCound);
      _queries.Verify(q => q.GetStudents(searchTermsDto, 1, 25), Times.Once, "GetStudents should be called once");
      _toDtoTranslator.Verify(t => t.ToStudentsDto(students), Times.Once, "ToStudentsDto should be called once");
    }

    [Fact(DisplayName = "ListStudentsAsync [Success]")]
    public async Task ListStudentsAsync_Success()
    {
      //Arrange
      var page = 1;
      var pageSize = 25;

      var students = new List<Student>
      {
        CommonTestsFactory.CreateStudent("M", 4),
        CommonTestsFactory.CreateStudent("F", 4),
        CommonTestsFactory.CreateStudent("F", 4),
        CommonTestsFactory.CreateStudent("F", 4),
        CommonTestsFactory.CreateStudent("F", 4),
      };

      var studentsDto = new List<StudentDto>
      {
        CommonTestsFactory.CreateStudentDto("M", 4, students[0]),
        CommonTestsFactory.CreateStudentDto("M", 4, students[1]),
        CommonTestsFactory.CreateStudentDto("F", 4, students[2]),
        CommonTestsFactory.CreateStudentDto("F", 4, students[3]),
        CommonTestsFactory.CreateStudentDto("M", 4, students[4])
      };

      _queries.Setup(q => q.ListStudents(1, 25)).ReturnsAsync((students, 5));
      _toDtoTranslator.Setup(t => t.ToStudentsDto(students)).ReturnsAsync(studentsDto);

      //Act
      var (content, totalCound) = await _services.ListStudentsAsync(pageSize, page);

      //Assert
      Assert.Equal(5, totalCound);
      _queries.Verify(q => q.ListStudents(1, 25), Times.Once, "GetStudents should be called once");
      _toDtoTranslator.Verify(t => t.ToStudentsDto(students), Times.Once, "ToStudentsDto should be called once");
    }

    [Fact(DisplayName = "InsertStudent [Success]")]
    public async Task InsertStudent_Success()
    {
      //Arrange
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 4);
      var student = CommonTestsFactory.CreateStudent(null, 0, studentDto);

      _toEntityTranslator.Setup(t => t.ToStudent(studentDto, null)).ReturnsAsync(student);
      _commands.Setup(c => c.AddStudentAsync(student)).ReturnsAsync(3);

      //Act
      var response = await _services.InsertStudentAsync(studentDto);

      //Assert
      Assert.Equal(3, response);
      _toEntityTranslator.Verify(t => t.ToStudent(studentDto, null), Times.Once, "ToStudentTranslator should be called once");
      _commands.Verify(c => c.AddStudentAsync(student), Times.Once, "AddStudentAsync should be called once");
    }

    [Fact(DisplayName = "UpdateStudent [Success]")]
    public async Task UpdateStudent_Success()
    {
      //Arrange
      var studentId = 567L;
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 4);
      var currentStudent = CommonTestsFactory.CreateStudent("M", 4);
      var updatedStudent = CommonTestsFactory.CreateStudent(null, 0, studentDto);

      _queries.Setup(q => q.GetStudent(studentId)).ReturnsAsync(currentStudent);
      _toEntityTranslator.Setup(t => t.ToStudent(studentDto, currentStudent)).ReturnsAsync(updatedStudent);

      //Act
      await _services.UpdateStudentAsync(studentId, studentDto);

      //Assert
      _queries.Verify(q => q.GetStudent(studentId), Times.Once, "GetStudent should be called once");
      _toEntityTranslator.Verify(t => t.ToStudent(studentDto, currentStudent), Times.Once, "ToStudentTranslator should be called once");
      _commands.Verify(c => c.SaveChangesAsync(), Times.Once, "SaveChangesAsync should be called once");
    }

    [Fact(DisplayName = "DeleteStudent [Success]")]
    public async Task DeleteStudent_Success()
    {
      //Arrange
      var studentId = 777L;
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 4);
      var currentStudent = CommonTestsFactory.CreateStudent("M", 4);

      _queries.Setup(q => q.GetStudent(studentId)).ReturnsAsync(currentStudent);

      //Act
      await _services.DeleteStudentAsync(studentId);

      //Assert
      _queries.Verify(q => q.GetStudent(studentId), Times.Once, "GetStudent should be called once");
      _commands.Verify(c => c.RemoveStudentAsync(currentStudent), Times.Once, "RemoveStudentAsync should be called once");
    }
    #endregion

    #region Course
    [Fact(DisplayName = "InsertCourse [Success]")]
    public async Task InsertCourse_Success()
    {
      //Arrange
      var courseDto = CommonTestsFactory.CreateCourseDto();
      var course = CommonTestsFactory.CreateCourse(courseDto);

      _toEntityTranslator.Setup(t => t.ToCourse(courseDto, null)).ReturnsAsync(course);
      _commands.Setup(c => c.AddCourseAsync(course)).ReturnsAsync(3);

      //Act
      var response = await _services.InsertCourseAsync(courseDto);

      //Assert
      Assert.Equal(3, response);
      _toEntityTranslator.Verify(t => t.ToCourse(courseDto, null), Times.Once, "ToCourseTranslator should be called once");
      _commands.Verify(c => c.AddCourseAsync(course), Times.Once, "AddCourseAsync should be called once");
    }

    [Fact(DisplayName = "UpdateCourse [Success]")]
    public async Task UpdateCourse_Success()
    {
      //Arrange
      var courseId = 567L;
      var courseDto = CommonTestsFactory.CreateCourseDto();
      var currentCourse = CommonTestsFactory.CreateCourse();
      var updatedCourse = CommonTestsFactory.CreateCourse(courseDto);

      _queries.Setup(q => q.GetCourse(courseId)).ReturnsAsync(currentCourse);
      _toEntityTranslator.Setup(t => t.ToCourse(courseDto, currentCourse)).ReturnsAsync(updatedCourse);

      //Act
      await _services.UpdateCourseAsync(courseId, courseDto);

      //Assert
      _queries.Verify(q => q.GetCourse(courseId), Times.Once, "GetCourse should be called once");
      _toEntityTranslator.Verify(t => t.ToCourse(courseDto, currentCourse), Times.Once, "ToCourseTranslator should be called once");
      _commands.Verify(c => c.SaveChangesAsync(), Times.Once, "SaveChangesAsync should be called once");
    }
    #endregion
  }
}
