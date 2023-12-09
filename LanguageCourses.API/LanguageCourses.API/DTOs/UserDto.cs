using LanguageCourses.API.Enums;

namespace UsedCars.API.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public Role Role { get; set; }
}