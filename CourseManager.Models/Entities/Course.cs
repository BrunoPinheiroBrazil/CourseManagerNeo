using System;

namespace CourseManager.Models.Entities
{
  public class Course
  {
    public long CourseId { get; set; }
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string TeacherName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}
