using LanguageCourses.API.DTOs;
using LanguageCourses.API.Extensions;
using LanguageCourses.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace LanguageCourses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ReviewController(IReviewRepository reviewRepository, IWebHostEnvironment webHostEnvironment)
    {
        _reviewRepository = reviewRepository;
        _hostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    [Route("first")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    public async Task<IActionResult> GetFirstReviews()
    {
        try
        {
            var reviews = await _reviewRepository.GetFirstReviewsAsync();

            if (reviews == null)
            {
                return NotFound();
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "UserPictures");

            foreach (var review in reviews)
            {
                if (review.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, review.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    review.Picture = base64Image;
                }
            }

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("courseReviews/id:Guid")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    public async Task<IActionResult> GetCourseReviews(Guid id)
    {
        try
        {
            var reviews = await _reviewRepository.GetCourseReviewsAsync(id);

            if (reviews == null)
            {
                return NotFound();
            }

            string projectPath = _hostEnvironment.ContentRootPath;
            string fullPath = Path.Combine(projectPath, "UserPictures");

            foreach (var review in reviews)
            {
                if (review.Picture != null)
                {
                    var imageBytes = System.IO.File.ReadAllBytes(Path.Combine(fullPath, review.Picture));
                    var base64Image = Convert.ToBase64String(imageBytes);

                    review.Picture = base64Image;
                }
            }

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,PROFESSOR,STUDENT")]
    [Route("addReview")]
    public async Task<IActionResult> AddCar([FromBody] AddReviewDto addReviewDto)
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var id = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(id);

            var review = addReviewDto.ConvertToReview(userId);
            await _reviewRepository.AddReviewAsync(review);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}