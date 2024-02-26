using LanguageCourses.API.Enums;

namespace LanguageCourses.API.DTOs;

public class UpdateCourseDto
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Duration { get; set; }
}