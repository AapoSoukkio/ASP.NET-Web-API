using Microsoft.AspNetCore.Mvc;
using UnivEnrollerApi.Data;
using UnivEnrollerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UnivEnrollerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GradeController : ControllerBase
{
    private readonly ILogger<GradeController> _logger;
    private readonly UnivEnrollerContext _db;

    public GradeController(ILogger<GradeController> logger, UnivEnrollerContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGrade(GradeModel model)
    {
        if (null == model)
        {
            return BadRequest();
        }

        var enrollment = await _db.Enrollments.FirstOrDefaultAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);
        if (enrollment == null)
        {
            // no existing enrollment -> cannot add grade 
            return NotFound();
        }

        enrollment.Grade = model.Grade;
        enrollment.GradingDate = model.GradingDate;

        await _db.SaveChangesAsync();

        return NoContent();
    }
}
