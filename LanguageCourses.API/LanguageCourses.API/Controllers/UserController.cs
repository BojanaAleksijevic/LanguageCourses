using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LanguageCourses.API.DTOs;
using LanguageCourses.API.Extensions;
using LanguageCourses.API.Repositories.Interfaces;
using UsedCars.API.DTOs;
using LanguageCourses.API.Repositories;
using System.Security.Cryptography;

namespace UsedCars.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Returns user token for verification
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            var user = await _userRepository.Authenticate(userLoginDto);

            if (user != null)
            {
                var token = _userRepository.Generate(user);

                return Ok(new
                {
                    Token = token,
                    user.FirstName,
                    user.LastName,
                    user.Role
                });
            }

            return NotFound("User not found!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Adds a new user to the database
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            await _userRepository.RegisterAsync(userRegisterDto);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Verifies the users account
    /// </summary>
    [HttpPost("verify")]
    public async Task<IActionResult> Verify(string token)
    {
        try
        {
            await _userRepository.VerifyAsync(token);

            return Ok("User verified!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Sends mail to reset password
    /// </summary>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        try
        {
            await _userRepository.ForgotPasswordAsync(email);

            return Ok("You may now reset your password!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Resets the users password
    /// </summary>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
    {
        try
        {
            await _userRepository.ResetPasswordAsync(userResetPasswordDto);

            return Ok("Password successfully reset!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Returns user information
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "ADMIN,PROFESSOR,STUDENT")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var userClaims = User as ClaimsPrincipal;
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid id = Guid.Parse(userId);

            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = user.ConvertToUserDto();

            return Ok(userDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProfessor")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> AddProfessor(AddProfessorDto addProfessorDto)
    {
        try
        {
            _userRepository.CreatePasswordHash(addProfessorDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var professor = addProfessorDto.ConvertToUser2(passwordHash, passwordSalt);
            await _userRepository.AddProfessorAsync(professor);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
