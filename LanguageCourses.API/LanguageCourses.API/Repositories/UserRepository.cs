using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LanguageCourses.API.Data;
using LanguageCourses.API.DTOs;
using LanguageCourses.API.Repositories.Interfaces;
using LanguageCourses.API.Models;

namespace LanguageCourses.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LanguageCoursesDbContext _languageCoursesDbContext;
        private readonly IConfiguration _configuration;

        public UserRepository(LanguageCoursesDbContext languageCoursesDbContext, IConfiguration configuration)
        {
            _languageCoursesDbContext = languageCoursesDbContext;
            _configuration = configuration;
        }

        public async Task AddUserAsync(User user)
        {
            await _languageCoursesDbContext.Users.AddAsync(user);
            await _languageCoursesDbContext.SaveChangesAsync();
        }

        public async Task<User> Authenticate(UserLoginDto loginDto)
        {
            var currentUser = await _languageCoursesDbContext.Users.FirstOrDefaultAsync(u => 
                u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (currentUser == null)
            {
                return null;
            }

            return currentUser;
        }

        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _languageCoursesDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }
    }
}
