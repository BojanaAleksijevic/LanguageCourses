using LanguageCourses.API.Enums;
using LanguageCourses.API.Models;
using UsedCars.API.DTOs;
using System.Security.Cryptography;
using LanguageCourses.API.DTOs;

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
            Role = Role.STUDENT,
            Picture = "unknown.png"
        };
    }

    public static User ConvertToUser2(this AddProfessorDto addProfessorDto, byte[] passwordHash, byte[] passwordSalt)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = addProfessorDto.FirstName,
            LastName = addProfessorDto.LastName,
            Phone = addProfessorDto.Phone,
            Email = addProfessorDto.Email,
            Picture = "unknown.png",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            VerificationToken = CreateRandomToken(),
            Role = Role.PROFESSOR,
            VerifiedAt = DateTime.Now
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
            Role = user.Role,
            Picture = user.Picture
        };
    }

    public static string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}