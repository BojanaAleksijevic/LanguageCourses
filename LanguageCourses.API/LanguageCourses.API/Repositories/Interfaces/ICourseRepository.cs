using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<CourseDto2>> GetAvailableCoursesAsync(
        int pageNumber,
        int pageSize,
        string? language = null,
        string? level = null,
        decimal? priceFrom = null,
        decimal? priceTo = null);
    Task<IEnumerable<CourseFirstDto>> GetFirstCoursesAsync();
    Task<CourseDto> GetCourseByIdAsync(Guid id);
    Task DeleteCourseAsync(Guid id);
}