using Microsoft.AspNetCore.Mvc;
using UnivEnrollerApi.Data;
using UnivEnrollerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UnivEnrollerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly UnivEnrollerContext _db;

    public CourseController(ILogger<CourseController> logger, UnivEnrollerContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Course>> Get()
    {
        return await _db.Cources.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Course> GetById(int id)
    {
        return await _db.Cources.FindAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CourseModel model)
    {
        Course course = new Course
        {
            Name = model.Name,
            UniversityId = model.UniversityId
        };
        _db.Cources.Add(course);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
                nameof(GetById),
                new { id = course.Id },
                course);
    }

}
