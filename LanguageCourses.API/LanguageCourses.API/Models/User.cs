using LanguageCourses.API.Enums;

namespace LanguageCourses.API.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }

    public List<Enrollment> Enrollments { get; } = new();
}