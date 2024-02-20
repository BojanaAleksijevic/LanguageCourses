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
    Task DeleteCourseAsync(Guid userId, Guid courseId);
    Task EnrollToCourseAsync(Guid userId, Guid courseId);
    Task AddCourseAsync(Course course);
    Task SetCourseAvailableAsync(Guid userId, Guid courseId);
    Task SetCourseDisabledAsync(Guid userId, Guid courseId);
    Task UpdateCourseAsync(UpdateCourseDto updateCourseDto, Guid userId);
}