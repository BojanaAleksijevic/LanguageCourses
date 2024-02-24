using LanguageCourses.API.DTOs;
using LanguageCourses.API.Extensions;
using LanguageCourses.API.Models;
using LanguageCourses.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

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
    [Route("{courseId:Guid}")]
    [ProducesResponseType(200, Type = typeof(CourseDto))]
    public async Task<IActionResult> GetCarById(Guid courseId)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid userId;

            if (id != null)
            {
                userId = Guid.Parse(id);
            }
            else
            {
                userId = Guid.Empty;
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string userPath = Path.Combine(projectPath, "UserPictures");
            string coursePath = Path.Combine(projectPath, "CoursePictures");

            var courseDto = await _courseRepository.GetCourseByIdAsync(courseId, userId);

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

            List<CourseDto2> result = new();

            foreach (var course in courses)
            {
                if (course.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, course.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    course.Picture = base64Image;
                }

                result.Add(course);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "STUDENT,PROFESSOR,ADMIN")]
    [Route("enrolment/{courseId:Guid}")]
    public async Task<IActionResult> EnrolToCourse([FromRoute] Guid courseId)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            await _courseRepository.EnrollToCourseAsync(userId, courseId);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("deleteCourse/{courseId:Guid}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] Guid courseId)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            await _courseRepository.DeleteCourseAsync(userId, courseId);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("addCourse")]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseDto addCourseDto)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid uid = Guid.Parse(userId);

            Guid cid = Guid.NewGuid();

            var course = addCourseDto.ConvertToCourse(uid, cid, _hostEnvironment);
            await _courseRepository.AddCourseAsync(course);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("setAvailable/{courseId:Guid}")]
    public async Task<IActionResult> SetCourseAvailable([FromRoute] Guid courseId)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            await _courseRepository.SetCourseAvailableAsync(userId, courseId);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("setDisabled/{courseId:Guid}")]
    public async Task<IActionResult> SetCourseDisabled([FromRoute] Guid courseId)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            await _courseRepository.SetCourseDisabledAsync(userId, courseId);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("updateCourse")]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto updateCourseDto)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            await _courseRepository.UpdateCourseAsync(updateCourseDto, userId);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,PROFESSOR,STUDENT")]
    [Route("userEnrolled")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseDto2>))]
    public async Task<IActionResult> GetUserEnrolled()
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            var courses = await _courseRepository.GetUserEnrolledCoursesAsync(userId);

            if (courses == null)
            {
                return NotFound();
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "CoursePictures");

            List<CourseDto2> result = new();

            foreach (var course in courses)
            {
                if (course.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, course.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    course.Picture = base64Image;
                }

                result.Add(course);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("userAvailable")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseDto2>))]
    public async Task<IActionResult> GetUserAvailableCourses()
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            var courses = await _courseRepository.GetAvailableCoursesAsync(userId);

            if (courses == null)
            {
                return NotFound();
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "CoursePictures");

            List<CourseDto2> result = new();

            foreach (var course in courses)
            {
                if (course.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, course.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    course.Picture = base64Image;
                }

                result.Add(course);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,PROFESSOR")]
    [Route("userDisabled")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseDto2>))]
    public async Task<IActionResult> GetUserDisabledCourses()
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            var courses = await _courseRepository.GetDisabledCoursesAsync(userId);

            if (courses == null)
            {
                return NotFound();
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "CoursePictures");

            List<CourseDto2> result = new();

            foreach (var course in courses)
            {
                if (course.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, course.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    course.Picture = base64Image;
                }

                result.Add(course);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}