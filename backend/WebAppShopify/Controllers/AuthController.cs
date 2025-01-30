using Microsoft.AspNetCore.Mvc;
using WebAppShopify.Services;
using System.Threading.Tasks;

namespace WebAppShopify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResponse = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (authResponse == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(authResponse);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
