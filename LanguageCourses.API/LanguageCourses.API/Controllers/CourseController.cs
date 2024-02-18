using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;
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
    private readonly IWebHostEnvironment _hostEnvironment;

    public CourseController(ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
    {
        _courseRepository = courseRepository;
        _hostEnvironment = webHostEnvironment;
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

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "CoursePictures");

            foreach (var course in courses)
            {
                if (course.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, course.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    course.Picture = base64Image;
                }
            }

            return Ok(courses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(200, Type = typeof(CourseDto))]
    public async Task<IActionResult> GetCarById(Guid id)
    {
        try
        {
            string projectPath = _hostEnvironment.ContentRootPath;
            string userPath = Path.Combine(projectPath, "UserPictures");
            string coursePath = Path.Combine(projectPath, "CoursePictures");

            var courseDto = await _courseRepository.GetCourseByIdAsync(id);

            if (courseDto == null)
            {
                return NotFound();
            }

            if (courseDto.ProfessorPicture != null)
            {
                var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(userPath, courseDto.ProfessorPicture));
                var base64Image = Convert.ToBase64String(imageBytes);
                courseDto.ProfessorPicture = base64Image;
            }

            if (courseDto.Picture != null)
            {
                var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(coursePath, courseDto.Picture));
                var base64Image = Convert.ToBase64String(imageBytes);
                courseDto.Picture = base64Image;
            }

            return Ok(courseDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("available")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseDto2>))]
    public async Task<IActionResult> GetAvailableCourses(
        [FromQuery] string? language,
        [FromQuery] string? level,
        [FromQuery] int? priceFrom,
        [FromQuery] int? priceTo,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize)
    {
        try
        {
            var courses = await _courseRepository.GetAvailableCoursesAsync(
                pageNumber,
                pageSize,
                language,
                level,
                priceFrom,
                priceTo);

            if (courses == null)
            {
                return NotFound();
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "CoursePictures");

            foreach (var course in courses)
            {
                if (course.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, course.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    course.Picture = base64Image;
                }
            }

            return Ok(courses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}