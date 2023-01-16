using CourseManager.DataBase.SqlServer.DataAccess;
using CourseManager.Models.Dtos;
using CourseManager.Models.Translators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagerServices
{
  public interface IServices
  {
    Task<long> InsertStudentAsync(StudentDto studentDto);
    Task<long> InsertCourseAsync(CourseDto courseDto);
    Task UpdateStudentAsync(long studentId, StudentDto studentDto);
    Task UpdateCourseAsync(long courseId, CourseDto courseDto);
    Task<StudentDto> GetStudent(long studentId);
    Task DeleteStudentAsync(long studentId);
    Task<(ICollection<StudentDto> studentsDto, int totalCount)> SearchStudentsAsync(SearchTermsDto searchTerms, int pageSize, int page);
    Task<(ICollection<StudentDto> studentsDto, int totalCount)> ListStudentsAsync(int pageSize, int page);
  }

  public class Services : IServices
  {
    private readonly IToEntityTranslator _toEntityTranslator;
    private readonly IToDtoTranslator _toDtoTranslator;
    private readonly IQueries _queries;
    private readonly ICommands _commands;
    public Services(IToEntityTranslator toEntityTranslator, IToDtoTranslator toDtoTranslator, ICommands commands, IQueries queries)
    {
      _toEntityTranslator = toEntityTranslator;
      _toDtoTranslator = toDtoTranslator;
      _queries = queries;
      _commands = commands;
    }

    public async Task<StudentDto> GetStudent(long studentId)
    {
      var student = await _queries.GetStudent(studentId);
      var studentDto = await _toDtoTranslator.ToStudentDto(student);

      return studentDto;
    }

    public async Task<(ICollection<StudentDto> studentsDto, int totalCount)> SearchStudentsAsync(SearchTermsDto searchTerms, int pageSize, int page)
    {
      var (students, totalCount) = await _queries.GetStudents(searchTerms, page, pageSize);

      var studentsDto = await _toDtoTranslator.ToStudentsDto(students);

      return (studentsDto, totalCount);
    }

    public async Task<long> InsertCourseAsync(CourseDto courseDto)
    {
      var course = await _toEntityTranslator.ToCourse(courseDto);

      return await _commands.AddCourseAsync(course);
    }

    public async Task UpdateCourseAsync(long courseId, CourseDto courseDto)
    {
      var course = await _queries.GetCourse(courseId);

      if (course == null)
        throw new Exception("Course does not exists!");

      await _toEntityTranslator.ToCourse(courseDto, course);
      await _commands.SaveChangesAsync();
    }

    public async Task<long> InsertStudentAsync(StudentDto studentDto)
    {
      var student = await _toEntityTranslator.ToStudent(studentDto);

      return await _commands.AddStudentAsync(student);
    }

    public async Task UpdateStudentAsync(long studentId, StudentDto studentDto)
    {
      var student = await _queries.GetStudent(studentId);

      if (student == null)
        throw new Exception("Student does not exists!");

      await _toEntityTranslator.ToStudent(studentDto, student);
      await _commands.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(long studentId)
    {
      var student = await _queries.GetStudent(studentId);
      await _commands.RemoveStudentAsync(student);
    }

    public async Task<(ICollection<StudentDto> studentsDto, int totalCount)> ListStudentsAsync(int pageSize, int page)
    {
      var (students, totalCount) = await _queries.ListStudents(page, pageSize);

      var studentsDto = await _toDtoTranslator.ToStudentsDto(students);

      return (studentsDto, totalCount);
    }
  }
}
