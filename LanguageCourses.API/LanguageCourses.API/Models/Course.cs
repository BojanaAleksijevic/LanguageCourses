namespace LanguageCourses.API.Models;

public class Course
{
    public Guid Id { get; set; }

    public Guid ProfessorId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Level { get; set; }

    public List<Enrollment> MyProperty { get; } = new();

    public List<Lesson> Lessons { get; } = new();

    public List<Forum> Forums { get; } = new();
}