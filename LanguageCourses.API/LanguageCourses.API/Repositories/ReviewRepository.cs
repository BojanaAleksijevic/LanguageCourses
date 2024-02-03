using LanguageCourses.API.Data;
using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;
using LanguageCourses.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.API.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly LanguageCoursesDbContext _languageCoursesDbContext;

    public ReviewRepository(LanguageCoursesDbContext languageCoursesDbContext)
    {
        _languageCoursesDbContext = languageCoursesDbContext;
    }

    public async Task<IEnumerable<ReviewDto>> GetFirstReviewsAsync()
    {
        var reviews = await _languageCoursesDbContext.Reviews
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .Join(_languageCoursesDbContext.Users,
                review => review.UserId,
                user => user.Id,
                (review, user) => new
                {
                    Review = review,
                    User = user
                })
                .Select(result => new ReviewDto
                {
                    Rating = result.Review.Rating,
                    Content = result.Review.Content,
                    PostDate = result.Review.PostDate,
                    FirstName = result.User.FirstName,
                    LastName = result.User.LastName,
                    Picture = result.User.Picture
                }).ToListAsync();

        return reviews;
    }
}