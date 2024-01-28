using LanguageCourses.API.DTOs;
using LanguageCourses.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewController(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    [HttpGet]
    [Route("first")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseFirstDto>))]
    public async Task<IActionResult> GetAvailableFirstCourses()
    {
        try
        {
            var reviews = await _reviewRepository.GetFirstReviewsAsync();

            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}