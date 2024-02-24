namespace LanguageCourses.API.DTOs;

public class ChangeUserPictureDto
{
    public Guid UserId { get; set; }

    public string Picture { get; set; }
}