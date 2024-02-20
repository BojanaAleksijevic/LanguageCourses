namespace LanguageCourses.API.DTOs;

public class AddReviewDto
{
    public Guid CourseId { get; set; }

    public int Rating { get; set; }

    public string Content { get; set; }
}