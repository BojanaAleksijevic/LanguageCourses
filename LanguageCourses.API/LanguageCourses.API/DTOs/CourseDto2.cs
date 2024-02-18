using LanguageCourses.API.Enums;

namespace LanguageCourses.API.DTOs;

public class CourseDto2
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Level { get; set; }

    public string Language { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public CourseType Type { get; set; }

    public decimal Price { get; set; }

    public string? Picture { get; set; } = null;
}