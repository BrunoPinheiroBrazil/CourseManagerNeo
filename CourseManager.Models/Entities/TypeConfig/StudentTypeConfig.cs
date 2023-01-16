using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Models.Entities.TypeConfig
{
  public class StudentTypeConfig : IEntityTypeConfiguration<Student>
  {
    public void Configure(EntityTypeBuilder<Student> builder)
    {
      builder.ToTable("Student");
      builder.HasKey(s => s.StudentId);
      builder.Property(s => s.FirstName).HasColumnType("varchar(50)");
      builder.Property(s => s.SurName).HasColumnType("varchar(50)");
      builder.Property(s => s.Gender).HasColumnType("varchar(2)");
      builder.Property(s => s.Dob).HasColumnType("dateTime");
      builder.Property(s => s.Address1).HasColumnType("varchar(150)");
      builder.Property(s => s.Address2).HasColumnType("varchar(150)");
      builder.Property(s => s.Address3).HasColumnType("varchar(150)");
    }
  }
}
