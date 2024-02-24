using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Extensions;

public static class CourseConversions
{
    public static Course ConvertToCourse(this AddCourseDto addCourseDto, Guid userId, Guid courseId, IWebHostEnvironment hostEnvironment)
    {
        return new Course
        {
            Id = courseId,
            ProfessorId = userId,
            Name = addCourseDto.Name,
            Description = addCourseDto.Description,
            Language = addCourseDto.Language,
            Level = addCourseDto.Level,
            Type = addCourseDto.Type,
            Price = addCourseDto.Price,
            Duration = addCourseDto.Duration,
            Picture = SaveCoursePicture(addCourseDto.Picture, courseId, hostEnvironment)
        };
    }

    private static string SaveCoursePicture(string picture, Guid courseId, IWebHostEnvironment hostEnvironment)
    {
        if(picture == null)
        {
            return "default.png";
        }

        string fileName = $"{courseId}.jpg";

        byte[] imageBytes = Convert.FromBase64String(picture);

        string projectPath = hostEnvironment.ContentRootPath;
        string fullPath = Path.Combine(projectPath, "CoursePictures");

        string imagePath = Path.Combine(fullPath, fileName);

        File.WriteAllBytes(imagePath, imageBytes);

        return fileName;
    }
}