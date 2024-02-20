using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Extensions;

public static class CourseConversions
{
    public static Course ConvertToCourse(this AddCourseDto addCourseDto, Guid userId, Guid id)
    {
        return new Course
        {
            Id = id,
            ProfessorId = userId,
            Name = addCourseDto.Name,
            Description = addCourseDto.Description,
            Language = addCourseDto.Language,
            Level = addCourseDto.Level,
            Type = addCourseDto.Type,
            Price = addCourseDto.Price,
            Duration = addCourseDto.Duration
        };
    }
}