using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Models.Entities.TypeConfig
{
  public class CourseTypeConfig : IEntityTypeConfiguration<Course>
  {
    public void Configure(EntityTypeBuilder<Course> builder)
    {
      builder.ToTable("Course");
      builder.HasKey(c => c.CourseId);
      builder.Property(c => c.CourseCode).HasColumnType("varchar(150)");
      builder.Property(c => c.CourseName).HasColumnType("varchar(150)");
      builder.Property(c => c.TeacherName).HasColumnType("varchar(150)");
      builder.Property(c => c.StartDate).HasColumnType("dateTime");
      builder.Property(c => c.EndDate).HasColumnType("dateTime");
    }
  }
}
