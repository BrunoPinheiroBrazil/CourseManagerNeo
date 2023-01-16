using CourseManager.Models.Dtos;
using CourseManagerServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseManager.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CourseManagerController : ControllerBase
  {
    private const int DEFAULT_PAGE_SIZE = 25;
    private const int DEFAULT_PAGE = 1;
    private readonly IServices _services;
    public CourseManagerController(IServices services)
    {
      _services = services;
    }

    [HttpPost("create/student")]
    public async Task<IActionResult> InsertStudent([FromBody] StudentDto studentDto)
    {
      return Ok(await _services.InsertStudentAsync(studentDto));
    }

    [HttpPut("update/student/{studentId}")]
    public async Task<IActionResult> UpdateStudent(long studentId, [FromBody] StudentDto studentDto)
    {
      await _services.UpdateStudentAsync(studentId, studentDto);
      return NoContent();
    }

    [HttpPost("create/course")]
    public async Task<IActionResult> InsertCourse([FromBody] CourseDto courseDto)
    {
      return Ok(await _services.InsertCourseAsync(courseDto));
    }

    [HttpPut("update/course/{courseId}")]
    public async Task<IActionResult> UpdateCourse(long courseId, [FromBody] CourseDto courseDto)
    {
      await _services.UpdateCourseAsync(courseId, courseDto);
      return NoContent();
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudent(long studentId)
    {
      return Ok(await _services.GetStudent(studentId));
    }

    [HttpGet("liststudents")]
    public async Task<IActionResult> ListStudents([FromQuery] int pageSize = DEFAULT_PAGE_SIZE, [FromQuery] int page = DEFAULT_PAGE)
    {
      var (students, totalCount) = await _services.ListStudentsAsync(pageSize, page);

      var paginatedResultsDto = new PaginatedResultsDto<StudentDto>
      {
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount,
        Values = students
      };

      return Ok(paginatedResultsDto);
    }

    [HttpPost("student")]
    public async Task<IActionResult> SearchStudents([FromBody] SearchTermsDto searchTerms, [FromQuery] int pageSize = DEFAULT_PAGE_SIZE, [FromQuery] int page = DEFAULT_PAGE)
    {
      var (students, totalCount) = await _services.SearchStudentsAsync(searchTerms, pageSize, page);
      var paginatedResultsDto = new PaginatedResultsDto<StudentDto>
      {
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount,
        Values = students
      };

      return Ok(paginatedResultsDto);
    }

    [HttpDelete("delete/student/{studentId}")]
    public async Task<IActionResult> DeleteStudent(long studentId)
    {
      await _services.DeleteStudentAsync(studentId);
      return NoContent();
    }
  }
}
