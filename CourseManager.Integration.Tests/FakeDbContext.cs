using CourseManager.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Integration.Tests
{
  public class FakeDbContext : CourseManagerDbContext
  {
    public FakeDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }
  }
}
