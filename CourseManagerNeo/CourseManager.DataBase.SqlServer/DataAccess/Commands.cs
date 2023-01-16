using CourseManager.Models.Entities;
using System.Threading.Tasks;

namespace CourseManager.DataBase.SqlServer.DataAccess
{
  public interface ICommands
  {
    Task SaveChangesAsync();
    Task<long> AddStudentAsync(Student student);
    Task<long> AddCourseAsync(Course course);
    Task RemoveStudentAsync(Student student);
  }
  public class Commands : ICommands
  {
    private readonly CourseManagerDbContext _context;

    public Commands(CourseManagerDbContext context)
    {
      _context = context;
    }

    public async Task<long> AddCourseAsync(Course course)
    {
      await _context.AddAsync(course);
      await SaveChangesAsync();
      return course.CourseId;
    }

    public async Task<long> AddStudentAsync(Student student)
    {
      await _context.AddAsync(student);
      await SaveChangesAsync();
      return student.StudentId;
    }

    public async Task RemoveStudentAsync(Student student)
    {
      _context.Remove(student);
      await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
