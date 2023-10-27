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
		#region Student
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

		[Fact(DisplayName = "ToStudentDto [Success] - Return Null when receive null student")]
		public async Task ToStudentDto_Success_Null_Value()
		{
			//Arrange
			//Act
			var studentDto = await _toDtoTranslator.ToStudentDto(null);

      //Assert
      Assert.Null(studentDto);
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
    #endregion

    #region Course
    [Fact(DisplayName = "ToCourseDto [Success]")]
    public async Task ToCourseDto_Success()
    {
			//Arrange
			var course = CommonTestsFactory.CreateCourse();

			//Act
			var courseDto = await _toDtoTranslator.ToCourseDto(course);

			//Assert
			await DtoAsserts.AssertCourseDto(course, courseDto);
		}

		[Fact(DisplayName = "ToCourseDto [Success] - Return null when null course")]
		public async Task ToCourseDto_Success_Null_Value()
		{
			//Arrange
			//Act
			var courseDto = await _toDtoTranslator.ToCourseDto(null);

			//Assert
			Assert.Null(courseDto);
		}
		#endregion
	}
}
