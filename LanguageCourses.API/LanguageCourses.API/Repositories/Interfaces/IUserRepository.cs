using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;
using UsedCars.API.DTOs;

namespace LanguageCourses.API.Repositories.Interfaces;

public interface IUserRepository
{
    string Generate(User user);
    Task<User> Authenticate(UserLoginDto loginDto);
    Task RegisterAsync(UserRegisterDto userRegisterDto);
    Task<User> GetUserAsync(Guid id);
    Task VerifyAsync(string token);
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(UserResetPasswordDto userResetPasswordDto);
}