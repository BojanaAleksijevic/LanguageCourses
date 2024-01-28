using LanguageCourses.API.Data;
using LanguageCourses.API.DTOs;
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

    public async Task<IEnumerable<CourseFirstDto>> GetFirstCoursesAsync()
    {
        var courses = await _languageCoursesDbContext.Courses
                .Where(c => c.Available)
                .Take(4)
                .Join(_languageCoursesDbContext.Users,
                course => course.ProfessorId,
                user => user.Id,
                (course, user) => new
                {
                    Course = course,
                    Professor = user
                })
                .Select(result => new CourseFirstDto
                {
                    Id = result.Course.Id,
                    Name = result.Course.Name,
                    Description = result.Course.Description,
                    Language = result.Course.Language,
                    FirstName = result.Professor.FirstName,
                    LastName = result.Professor.LastName,
                    Picture = result.Professor.Picture
                }).ToListAsync();

        return courses;
    }
}