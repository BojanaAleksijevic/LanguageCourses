using LanguageCourses.API.Enums;

namespace LanguageCourses.API.DTOs;

public class AddCourseDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Language { get; set; } = null;

    public string Level { get; set; }

    public CourseType Type { get; set; }

    public decimal Price { get; set; }

    public int Duration { get; set; }

    public string? Picture { get; set; }
}