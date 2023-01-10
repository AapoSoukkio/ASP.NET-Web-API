using Microsoft.AspNetCore.Mvc;
using UnivEnrollerApi.Data;
using Microsoft.EntityFrameworkCore;

namespace UnivEnrollerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly UnivEnrollerContext _db;

    public StudentController(ILogger<StudentController> logger, UnivEnrollerContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Student>> Get()
    {
        return await _db.Students.ToListAsync();
    }

    [HttpGet("{id}/courses")]
    public async Task<IActionResult> GetStudentCourses(int id)
    {
        return Ok(await _db.Enrollments
                        .Include(e => e.Course)
                        .Where(e => e.StudentId == id)
                        .Select(e => new { e.Id, CourseId = e.Course.Id, Course = e.Course.Name, e.Grade, e.GradingDate })
                        .ToListAsync());
    }

    [HttpDelete("{id}/course/{courseId}")]
    public async Task<IActionResult> DeleteStudentCourseEnrollment(int id, int courseId)
    {
        var enrollment = await _db.Enrollments.FirstOrDefaultAsync(e => e.StudentId == id && e.CourseId == courseId);
        if (null == enrollment)
        {
            return NotFound();
        }
        if (null != enrollment.Grade)
        {
            // allready graded -> cannot delete
            return NoContent();
        }
        _db.Enrollments.Remove(enrollment);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
