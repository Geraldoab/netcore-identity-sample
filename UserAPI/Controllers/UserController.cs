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
            var result = await _userService.AddAsync(dto);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            return result.Succeeded ? Ok(result.Content) : BadRequest(result.Errors);
        }
    }
}
