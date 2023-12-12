using System.ComponentModel.DataAnnotations;

namespace UsedCars.API.DTOs;

public class UserRegisterDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters for the password!")]
    public string Password { get; set; }

    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; }
}