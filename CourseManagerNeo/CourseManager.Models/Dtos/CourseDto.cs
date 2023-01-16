using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Models.Dtos
{
  public class CourseDto
  {
    public string CourseId { get; set; }
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string TeacherName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}
