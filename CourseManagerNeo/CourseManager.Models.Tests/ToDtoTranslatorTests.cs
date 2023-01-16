using CourseManager.Common.Tests;
using CourseManager.Models.Entities;
using CourseManager.Models.Translators;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CourseManager.Models.Tests
{
  public class ToDtoTranslatorTests
  {
    private readonly ToDtoTranslator _toDtoTranslator;

    public ToDtoTranslatorTests()
    {
      _toDtoTranslator = new ToDtoTranslator();
    }

    [Fact(DisplayName ="ToStudentDto [Success]")]
    public async Task ToStudentDto_Success()
    {
      //Arrange
      var student = CommonTestsFactory.CreateStudent("F", 4);

      //Act
      var studentDto = await _toDtoTranslator.ToStudentDto(student);

      //Assert
      await DtoAsserts.AssertStudentDto(student, studentDto);
    }

    [Fact(DisplayName = "ToStudentsDto [Success]")]
    public async Task ToStudentsDto_Success()
    {
      //Arrange
      var students = new List<Student>
      {
        CommonTestsFactory.CreateStudent("F", 15),
        CommonTestsFactory.CreateStudent("F", 15),
        CommonTestsFactory.CreateStudent("F", 15),
      };

      //Act
      var studentsDto = await _toDtoTranslator.ToStudentsDto(students);

      //Assert
      await DtoAsserts.AssertStudentsDto(students, studentsDto);
    }
  }
}
