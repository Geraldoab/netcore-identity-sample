using Microsoft.AspNetCore.Mvc;
using UserAPI.Data.Dtos;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUserAsync(CreateUserDto dto)
        {
            await _userService.AddAsync(dto);
            return Ok("User was added with success.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            return Ok(token);
        }
    }
}
