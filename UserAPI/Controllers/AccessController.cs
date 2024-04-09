using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AccessController : ControllerBase
    {
        /// <summary>
        /// Check if a user has authorization
        /// </summary>
        /// <returns>Returns a message showing that the user has authorization</returns>
        [HttpGet]
        [Authorize(Policy = "MinimumAge")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok("It is everything ok.");
        }
    }
}
