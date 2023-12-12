using LanguageCourses.API.Enums;
using LanguageCourses.API.Models;
using UsedCars.API.DTOs;
using System.Security.Cryptography;

namespace LanguageCourses.API.Extensions;

public static class UserConversions
{
    public static User ConvertToUser(this UserRegisterDto userRegisterDto, byte[] passwordHash, byte[] passwordSalt)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Phone = userRegisterDto.Phone,
            Email = userRegisterDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            VerificationToken = CreateRandomToken(),
            Role = Role.STUDENT
        };
    }

    public static UserDto ConvertToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            Role = user.Role
        };
    }

    public static string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}