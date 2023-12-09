using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LanguageCourses.API.DTOs;
using LanguageCourses.API.Extensions;
using LanguageCourses.API.Repositories.Interfaces;
using UsedCars.API.DTOs;

namespace UsedCars.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userRepository.Authenticate(userLoginDto);

            if (user != null)
            {
                var token = _userRepository.Generate(user);

                return Ok(new
                {
                    Token = token,
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Role,
                    user.Email
                });
            }

            return NotFound("User not found!");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var user = userRegisterDto.ConvertToUser();
                await _userRepository.AddUserAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
    }
}
