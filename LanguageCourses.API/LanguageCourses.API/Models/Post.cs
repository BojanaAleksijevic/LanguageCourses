namespace LanguageCourses.API.Models;

public class Post
{
    public Guid Id { get; set; }

    public Guid ForumId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; }

    public DateTime PostDate { get; set; }

    public Forum Forum { get; set; } = null!;
}