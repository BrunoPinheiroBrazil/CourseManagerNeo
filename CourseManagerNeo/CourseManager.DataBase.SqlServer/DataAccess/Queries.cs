using CourseManager.Models.Dtos;
using CourseManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseManager.DataBase.SqlServer.DataAccess
{
  public interface IQueries
  {
    Task<Student> GetStudent(long studentId);
    Task<Course> GetCourse(long courseId);
    Task<(ICollection<Student>, int)> GetStudents(SearchTermsDto searchTermsDto, int page = 1, int pageSize = 25);
    Task<(ICollection<Student>, int)> ListStudents(int page = 1, int pageSize = 25);
  }
  public class Queries : IQueries
  {
    private readonly CourseManagerDbContext _context;

    public Queries(CourseManagerDbContext context)
    {
      _context = context;
    }

    public async Task<Course> GetCourse(long courseId)
    {
      return await CourseWithIncludes().FirstOrDefaultAsync(c => c.CourseId == courseId);
    }

    public async Task<Student> GetStudent(long studentId)
    {
      return await StudentWithIncludes().FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    private IQueryable<Student> StudentWithIncludes()
    {
      return _context.Students
        .Where(s => s.FirstName != null && s.SurName != null)
        .AsQueryable();
    }

    private IQueryable<Course> CourseWithIncludes()
    {
      return _context.Courses
        .Where(s => s.CourseCode != null)
        .AsQueryable();
    }

    public async Task<(ICollection<Student>, int)> GetStudents(SearchTermsDto searchTermsDto, int page = 1, int pageSize = 25)
    {
      if (page < 1)
        page = 1;

      var orderedStudents = await SearchStudentQuery(searchTermsDto.FirstName, page, pageSize)
        .OrderBy(s => s.FirstName).ToListAsync();
      var totalCount = orderedStudents.Count;

      return (orderedStudents, totalCount);
    }

    private IQueryable<Student> SearchStudentQuery(string firstName = null, int page =  1, int pageSize = 25)
    {
      var allQueriesOR = new List<Expression<Func<Student, bool>>>();
      if (!string.IsNullOrEmpty(firstName))
      {
        ByFirstNameQuery(firstName, allQueriesOR);
      }

      if (allQueriesOR.Count == 0)
      {
        throw new ArgumentException("The serch query has no arguments. You can't call a search without filters");
      }

      var skippedRecords = pageSize * (page - 1);
      return StudentWithIncludes().Where(allQueriesOR.Aggregate((q1, q2) => q1.Or(q2))).Skip(skippedRecords).Take(pageSize);
    }

    private void ByFirstNameQuery(string firstName, List<Expression<Func<Student, bool>>> allQueriesOR)
    {
      Expression<Func<Student, bool>> byFirstNameOR()
      {
        return s => s.FirstName.Contains(firstName);
      }

      allQueriesOR.Add(byFirstNameOR());
    }

    public async Task<(ICollection<Student>, int)> ListStudents(int page = 1, int pageSize = 25)
    {
      if (page < 1)
        page = 1;

      var skippedRecords = pageSize * (page - 1);
      
      var orderedStudents = await StudentWithIncludes().Skip(skippedRecords).Take(pageSize).OrderBy(s => s.FirstName).ToListAsync();
      
      var totalCount = orderedStudents.Count;

      return (orderedStudents, totalCount);
    }
  }
}
