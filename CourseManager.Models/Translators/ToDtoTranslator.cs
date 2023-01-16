using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CourseManager.Models.Translators
{
  public interface IToDtoTranslator
  {
    Task<StudentDto> ToStudentDto(Student student);
    Task<ICollection<StudentDto>> ToStudentsDto(ICollection<Student> students);
  }
  public class ToDtoTranslator : IToDtoTranslator
  {
    public async Task<StudentDto> ToStudentDto(Student student)
    {
      if (student == null)
        return null;

      return await Task.FromResult(new StudentDto
      {
        FirstName = student.FirstName,
        SurName = student.SurName,
        Gender = student.Gender,
        Dob = student.Dob,
        Address1 = student.Address1,
        Address2 = student.Address2,
        Address3 = student.Address3,
        StudentId = student.StudentId
      });
    }

    public async Task<ICollection<StudentDto>> ToStudentsDto(ICollection<Student> students)
    {
      if (students == null)
        return null;

      var studentsDto = new List<StudentDto>();

      var tasks = new List<Task>();

      students.ToList().ForEach(s =>
      {
        tasks.Add(Task.Run(
          () =>
          {
            studentsDto.Add(
              new StudentDto
              {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                SurName = s.SurName,
                Gender = s.Gender,
                Address1 = s.Address1,
                Address2 = s.Address2,
                Address3 = s.Address3,
                Dob = s.Dob
              }
            );
          })
        );
      });

      Task t = Task.WhenAll(tasks);

      await t;

      return studentsDto;
    }
  }
}
