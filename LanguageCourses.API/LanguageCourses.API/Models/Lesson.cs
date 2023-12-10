namespace LanguageCourses.API.Models;

public class Lesson
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public Course Course { get; set; } = null!;
}