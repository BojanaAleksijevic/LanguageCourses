namespace LanguageCourses.API.Models;

public class Review
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string Content { get; set; }

    public DateTime PostDate { get; set; }

    public User User { get; set; } = null!;

    public Course Course { get; set; } = null!;
}