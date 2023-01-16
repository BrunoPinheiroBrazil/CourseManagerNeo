using System;

namespace CourseManager.Models.Dtos
{
  public class StudentDto
  {
    public long StudentId { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }
    public DateTime Dob { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
  }
}