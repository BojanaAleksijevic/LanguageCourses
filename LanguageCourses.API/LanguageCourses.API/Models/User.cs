using LanguageCourses.API.Enums;

namespace LanguageCourses.API.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string? Picture { get; set; } = null;

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string? VerificationToken { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? ResetTokenExpires { get; set; }

    public Role Role { get; set; }

    public List<Course> Courses { get; } = new();

    public List<CourseUser> CourseUsers { get; } = new();
}