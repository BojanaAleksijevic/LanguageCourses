using LanguageCourses.API.Enums;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.DTOs;

public class CourseDto
{
    public Guid Id { get; set; }

    public string ProfessorFirstName { get; set; }

    public string ProfessorLastName { get; set; }

    public string ProfessorPhone { get; set; }

    public string ProfessorEmail { get; set; }

    public string ProfessorPicture { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Language { get; set; } = null;

    public string Level { get; set; }

    public CourseType Type { get; set; }

    public decimal Price { get; set; }

    public int Duration { get; set; }

    public string? Picture { get; set; } = null;
}