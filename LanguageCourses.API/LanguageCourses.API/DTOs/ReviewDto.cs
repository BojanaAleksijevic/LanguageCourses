namespace LanguageCourses.API.DTOs;

public class ReviewDto
{
    public int Rating { get; set; }

    public string Content { get; set; }

    public DateTime PostDate { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Picture { get; set; } = null;
}