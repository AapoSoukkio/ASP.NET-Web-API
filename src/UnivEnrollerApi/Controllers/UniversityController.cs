using Microsoft.AspNetCore.Mvc;
using UnivEnrollerApi.Data;
using Microsoft.EntityFrameworkCore;

namespace UnivEnrollerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UniversityController : ControllerBase
{
    private readonly ILogger<UniversityController> _logger;
    private readonly UnivEnrollerContext _db;

    public UniversityController(ILogger<UniversityController> logger, UnivEnrollerContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public async Task<IEnumerable<University>> Get()
    {
        return await _db.Universities.ToListAsync();
    }

    [HttpGet("{id}/courses")]
    public async Task<IEnumerable<Course>> GetUniversityCourses(int id)
    {
        return await _db.Cources.Where(c => c.UniversityId == id).ToListAsync();
    }

}
