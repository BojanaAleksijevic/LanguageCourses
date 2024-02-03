using LanguageCourses.API.Enums;

namespace LanguageCourses.API.Models;

public class Course
{
    public Guid Id { get; set; }

    public Guid ProfessorId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Language { get; set; } = null;

    public string Level { get; set; }

    public CourseType Type { get; set; }

    public int Price { get; set; }

    public int Duration { get; set; }

    public bool Available { get; set; } = false;

    public string? Picture { get; set; } = null;

    public List<User> Users { get; } = new();

    public List<CourseUser> CourseUsers { get; } = new();

    public List<Lesson> Lessons { get; } = new();

    public List<Review> Reviews { get; } = new();
}