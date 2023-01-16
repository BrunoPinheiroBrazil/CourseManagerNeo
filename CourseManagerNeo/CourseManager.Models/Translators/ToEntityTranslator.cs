using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using System.Threading.Tasks;

namespace CourseManager.Models.Translators
{
  public interface IToEntityTranslator
  {
    Task<Student> ToStudent(StudentDto studentDto, Student student = null);
    Task<Course> ToCourse(CourseDto courseDto, Course course = null);
  }
  public class ToEntityTranslator : IToEntityTranslator
  {
    public async Task<Course> ToCourse(CourseDto courseDto, Course course = null)
    {
      if (course == null)
        return await Task.FromResult(new Course
        {
          CourseCode = courseDto.CourseCode,
          CourseName = courseDto.CourseName,
          TeacherName = courseDto.TeacherName,
          StartDate = courseDto.StartDate,
          EndDate = courseDto.EndDate
        });

      course.CourseCode = courseDto.CourseCode;
      course.CourseName = courseDto.CourseName;
      course.TeacherName = courseDto.TeacherName;
      course.StartDate = courseDto.StartDate;
      course.EndDate = courseDto.EndDate;

      return await Task.FromResult(course);
    }

    public async Task<Student> ToStudent(StudentDto studentDto, Student student = null)
    {
      if (student == null)
        return await Task.FromResult(new Student
        {
          FirstName = studentDto.FirstName,
          SurName = studentDto.SurName,
          Gender = studentDto.Gender,
          Dob = studentDto.Dob,
          Address1 = studentDto.Address1,
          Address2 = studentDto.Address2,
          Address3 = studentDto.Address3
        });

      student.FirstName = studentDto.FirstName;
      student.SurName = studentDto.SurName;
      student.Gender = studentDto.Gender;
      student.Dob = studentDto.Dob;
      student.Address1 = studentDto.Address1;
      student.Address2 = studentDto.Address2;
      student.Address3 = studentDto.Address3;

      return await Task.FromResult(student);
    }
  }
}
