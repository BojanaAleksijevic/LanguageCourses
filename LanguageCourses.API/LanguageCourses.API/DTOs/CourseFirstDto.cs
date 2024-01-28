namespace LanguageCourses.API.DTOs;

public class CourseFirstDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Language { get; set; } = null;

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Picture { get; set; } = null;
}