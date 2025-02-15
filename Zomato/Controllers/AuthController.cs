using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zomato.Dto;
using Zomato.Service;

namespace Zomato.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponceDto> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var tokens = _authService.login(loginRequestDto.email, loginRequestDto.password);
                return Ok(new LoginResponceDto { accessToken = tokens[0] });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password.");
            }
        }
        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpDto signUpDto)
        {
            try
            {
                var user = _authService.SignUp(signUpDto);
                return Created("User Created", user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}