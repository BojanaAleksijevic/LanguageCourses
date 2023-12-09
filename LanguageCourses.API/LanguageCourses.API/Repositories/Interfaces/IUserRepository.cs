using LanguageCourses.API.DTOs;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Repositories.Interfaces;

public interface IUserRepository
{
    string Generate(User user);
    Task<User> Authenticate(UserLoginDto loginDto);
    Task AddUserAsync(User user);
    Task<User> GetUserAsync(Guid id);
}