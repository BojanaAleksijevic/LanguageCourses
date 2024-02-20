using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LanguageCourses.API.Data;
using LanguageCourses.API.DTOs;
using LanguageCourses.API.Repositories.Interfaces;
using LanguageCourses.API.Models;
using System.Security.Cryptography;
using UsedCars.API.DTOs;
using LanguageCourses.API.Extensions;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace LanguageCourses.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LanguageCoursesDbContext _languageCoursesDbContext;
    private readonly IConfiguration _configuration;

    public UserRepository(LanguageCoursesDbContext languageCoursesDbContext, IConfiguration configuration)
    {
        _languageCoursesDbContext = languageCoursesDbContext;
        _configuration = configuration;
    }

    public async Task RegisterAsync(UserRegisterDto userRegisterDto)
    {
        if (_languageCoursesDbContext.Users.Any(u => u.Email == userRegisterDto.Email))
        {
            throw new Exception("The user with this email address already exists!");
        }

        CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = userRegisterDto.ConvertToUser(passwordHash, passwordSalt);

        await _languageCoursesDbContext.Users.AddAsync(user);
        await _languageCoursesDbContext.SaveChangesAsync();

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("languagecourses.fin@gmail.com"));
        email.To.Add(MailboxAddress.Parse(user.Email));
        email.Subject = "Kurs jezika - potvrda naloga";
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = $@"
                    <!DOCTYPE html>
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    </head>
                    <body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; text-align: center;"">

                        <h1 style=""color: #333;"">Potvrda naloga</h1>

                        <p style=""color: #555;"">Poštovani {user.FirstName} {user.LastName},</p>

                        <p style=""color: #555;"">Dobrodošli na Kurs jezika! Molimo Vas da potvrdite nalog klikom na sledeći link:</p>

                        <a href=""http://localhost:3000/verifikacija?token={user.VerificationToken}"" 
                            style=""display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;"">
                            Potvrdi nalog
                        </a>
                        <p style=""color: #555; margin-top: 20px;"">Ukoliko niste Vi izvršili registraciju, ignorišite ovu poruku.</p>

                    </body>
                    </html>
                    "
        };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("languagecourses.fin@gmail.com", "qrzi kaan blff xifm");
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public async Task<User> Authenticate(UserLoginDto loginDto)
    {
        var user = await _languageCoursesDbContext.Users.FirstOrDefaultAsync(u =>
            u.Email == loginDto.Email);

        if (user == null)
        {
            return null;
        }

        if (user.VerifiedAt == null)
        {
            throw new Exception("The user is not verified!");
        }

        if (VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt) == false)
        {
            throw new Exception("Password is incorrect!");
        }

        return user;
    }

    public async Task VerifyAsync(string token)
    {
        var user = _languageCoursesDbContext.Users.FirstOrDefault(u => u.VerificationToken == token);

        if (user == null)
        {
            throw new Exception("Invalid token!");
        }

        user.VerifiedAt = DateTime.Now;
        await _languageCoursesDbContext.SaveChangesAsync();
    }

    public async Task ForgotPasswordAsync(string userEmail)
    {
        var user = _languageCoursesDbContext.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null)
        {
            throw new Exception("User not found!");
        }

        user.PasswordResetToken = UserConversions.CreateRandomToken();
        user.ResetTokenExpires = DateTime.Now.AddDays(1);
        await _languageCoursesDbContext.SaveChangesAsync();

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("languagecourses.fin@gmail.com"));
        email.To.Add(MailboxAddress.Parse(user.Email));
        email.Subject = "Kurs jezika - promena lozinke";
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = $@"
                    <!DOCTYPE html>
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    </head>
                    <body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; text-align: center;"">

                        <h1 style=""color: #333;"">Promena lozinke</h1>

                        <p style=""color: #555;"">Poštovani {user.FirstName} {user.LastName},</p>

                        <p style=""color: #555;"">Da bi promenili lozinku potrebno je da kliknete na sledeći link:</p>

                        <a href=""http://localhost:3000/zaboravljena?token={user.PasswordResetToken}"" 
                            style=""display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;"">
                            Potvrdi nalog
                        </a>
                        <p style=""color: #555; margin-top: 20px;"">Ukoliko niste Vi zatražili promenu lozinke, ignorišite ovu poruku.</p>

                    </body>
                    </html>
                    "
        };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("languagecourses.fin@gmail.com", "qrzi kaan blff xifm");
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public async Task ResetPasswordAsync(UserResetPasswordDto request)
    {
        var user = _languageCoursesDbContext.Users.FirstOrDefault(
            u => u.PasswordResetToken == request.Token);

        if (user == null)
        {
            throw new Exception("Invalid token!");
        }

        if (user.ResetTokenExpires < DateTime.Now)
        {
            throw new Exception("The token has expired!");
        }

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.PasswordResetToken = null;
        user.ResetTokenExpires = null;

        await _languageCoursesDbContext.SaveChangesAsync();
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

    public async Task AddProfessorAsync(User professor)
    {
        await _languageCoursesDbContext.Users.AddAsync(professor);
        await _languageCoursesDbContext.SaveChangesAsync();
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}