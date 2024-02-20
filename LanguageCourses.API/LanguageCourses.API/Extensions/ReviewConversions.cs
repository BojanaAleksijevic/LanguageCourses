using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Extensions;

public static class ReviewConversions
{
    public static Review ConvertToReview(this AddReviewDto addReviewDto, Guid userId)
    {
        return new Review
        {
            Id = Guid.NewGuid(),
            CourseId = addReviewDto.CourseId,
            UserId = userId,
            Rating = addReviewDto.Rating,
            Content = addReviewDto.Content,
            PostDate = DateTime.Now
        };
    }
}