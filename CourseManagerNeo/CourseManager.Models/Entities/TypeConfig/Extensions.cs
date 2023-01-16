using Microsoft.EntityFrameworkCore;

namespace CourseManager.Models.Entities.TypeConfig
{
  public static class Extensions
  {
    public static void ConfigureAllTypeConfig(this ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new StudentTypeConfig());
      modelBuilder.ApplyConfiguration(new CourseTypeConfig());
    }
  }
}
