using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Repositories.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<ReviewDto>> GetFirstReviewsAsync();
    Task<IEnumerable<ReviewDto>> GetCourseReviewsAsync(Guid courseId);
}