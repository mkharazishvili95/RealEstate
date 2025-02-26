using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Models.Auth;
using RealEstate.Application.Services;
using RealEstate.Core.User;

namespace RealEstate.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IJwtTokenService _jwtTokenService;
        readonly IUserService _userService;

        public AuthController(IJwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _userService.Register(model);
            if (!success)
                return Conflict(new { message = "Email already exists" });

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
                return Unauthorized();

            var accessToken = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            _userService.Update(user);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }
    }
}
