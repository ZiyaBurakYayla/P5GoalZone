using GoalZone.API.DTOs.AuthDtos;
using GoalZone.API.Services;
using GoalZone.API.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var valid = await _authService.ValidateUserAsync(request.Username, request.Password);
            if (!valid) return Unauthorized(new { error = "Invalid credentials." });
            return Ok(new { message = "Login successful." });
        }
    }
}