using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAvailableCoursesAsync(
        int pageNumber,
        int pageSize,
        string? mark = null,
        string? type = null);
    Task<IEnumerable<CourseFirstDto>> GetFirstCoursesAsync();
}