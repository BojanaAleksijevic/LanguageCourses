using LanguageCourses.API.DTOs;
using LanguageCourses.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LanguageCourses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    [Route("availablefirst")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseFirstDto>))]
    public async Task<IActionResult> GetAvailableFirstCourses()
    {
        try
        {
            var courses = await _courseRepository.GetFirstCoursesAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}