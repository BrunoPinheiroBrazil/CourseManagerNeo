using CourseManager.Common.Tests;
using CourseManager.Models.Translators;
using System.Threading.Tasks;
using Xunit;

namespace CourseManager.Models.Tests
{
  public class ToEntityTranslatorTests
  {
    private readonly ToEntityTranslator _translator;
    public ToEntityTranslatorTests()
    {
      _translator = new ToEntityTranslator();
    }

    [Fact(DisplayName = "ToStudent [Success]")]
    public async Task ToStudent_Success()
    {
      //Arrange
      var studentDto = CommonTestsFactory.CreateStudentDto("M", 5);

      //Act
      var student = await _translator.ToStudent(studentDto);

      //Assert
      await EntityAsserts.AssertStudentAsync(studentDto, student);
    }

    [Fact(DisplayName = "ToCourse [Success]")]
    public async Task ToCourse_Success()
    {
      //Arrange
      var courseDto = CommonTestsFactory.CreateCourseDto();

      //Act
      var course = await _translator.ToCourse(courseDto);

      //Assert
      await EntityAsserts.AssertCoursesAsync(courseDto, course);
    }
  }
}
