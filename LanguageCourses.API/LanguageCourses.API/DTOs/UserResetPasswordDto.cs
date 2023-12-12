using System.ComponentModel.DataAnnotations;

namespace LanguageCourses.API.DTOs;

public class UserResetPasswordDto
{
    [Required]
    public string Token { get; set; }

    [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters for the password!")]
    public string Password { get; set; }

    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; }
}