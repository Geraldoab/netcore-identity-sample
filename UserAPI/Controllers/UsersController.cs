using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Domain.Interfaces.Services;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="dto">The request to create a new User.</param>
        /// <returns>Returns HTTP Status 200 when a user was created successfully.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserAsync(CreateUserDto dto)
        {
            var result = await _userService.AddAsync(dto);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        /// <summary>
        /// Authenticate a user.
        /// </summary>
        /// <param name="dto">The request with username and password</param>
        /// <returns>Returns HTTP Status 200 when a user was authenticated successfully.</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync(UserLoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            return result.Succeeded ? Ok(result.Content) : BadRequest(result.Errors);
        }
    }
}
