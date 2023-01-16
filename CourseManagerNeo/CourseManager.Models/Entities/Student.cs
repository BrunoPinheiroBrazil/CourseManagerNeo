using System;

namespace CourseManager.Models.Entities
{
  public class Student
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
