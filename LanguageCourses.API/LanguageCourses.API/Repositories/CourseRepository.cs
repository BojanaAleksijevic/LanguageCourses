using LanguageCourses.API.Data;
using LanguageCourses.API.Models;
using LanguageCourses.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.API.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly LanguageCoursesDbContext _languageCoursesDbContext;

    public CourseRepository(LanguageCoursesDbContext languageCoursesDbContext)
    {
        _languageCoursesDbContext = languageCoursesDbContext;
    }

    public async Task<IEnumerable<Course>> GetAvailableCoursesAsync(
        int pageNumber,
        int pageSize,
        string? mark = null,
        string? type = null)
    {
        var courses = _languageCoursesDbContext.Courses
                .Where(c => c.Available)
                .AsQueryable();

        /*if (string.IsNullOrWhiteSpace(mark) == false)
        {
            courses = courses.Where(x => x.Mark.Contains(mark));
        }

        if (string.IsNullOrWhiteSpace(type) == false)
        {
            courses = courses.Where(x => x.CarBody.Contains(type));
        }*/

        var skipResults = (pageNumber - 1) * pageSize;

        return await courses.Skip(skipResults).Take(pageSize).ToListAsync();
    }
}