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

    public async Task<IEnumerable<ReviewDto>> GetCourseReviewsAsync(Guid courseId)
    {
        var reviews = await _languageCoursesDbContext.Reviews
                .Where(r => r.CourseId == courseId)
                .OrderBy(r => r.PostDate)
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

    public async Task AddReviewAsync(Review review)
    {
        var courseUser = await _languageCoursesDbContext.Enrollments.FirstOrDefaultAsync(
            x => x.UserId == review.UserId && x.CourseId == review.CourseId);

        if (courseUser == null)
        {
            throw new Exception("You are not enroled on this course, so you can't write reviews!");
        }

        await _languageCoursesDbContext.Reviews.AddAsync(review);
        await _languageCoursesDbContext.SaveChangesAsync();
    }
}