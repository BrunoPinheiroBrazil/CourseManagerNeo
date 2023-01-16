using CourseManager.Models.Entities;
using CourseManager.Models.Entities.TypeConfig;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.DataBase.SqlServer
{
  public class CourseManagerDbContext : DbContext
  {
    public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BrunoEstudos;Trusted_Connection=True;MultipleActiveResultSets=true");
        base.OnConfiguring(optionsBuilder);
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ConfigureAllTypeConfig();
    }
  }
}
