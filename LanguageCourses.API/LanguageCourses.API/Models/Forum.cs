namespace LanguageCourses.API.Models;

public class Forum
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public Course Course { get; set; } = null!;

    public List<Post> Posts { get; } = new();
}