using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleUser.API.Auths;
using SimpleUser.API.DTOs;
using SimpleUser.API.Heplers;
using SimpleUser.API.Services;
using SimpleUser.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleUser.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static RefreshToken _refreshToken = new RefreshToken();
        private IUserService _userService;
        private IJwtService _jwtService;
        public IConfiguration _configuration;

        public AuthController(IUserService userService, IJwtService jwtService, IConfiguration configuration)
        {
            _userService = userService;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            if (userLogin != null && userLogin.Email != null && userLogin.Password != null)
            {
                User user = await _userService.Login(userLogin.Email, userLogin.Password);

                if (user != null)
                {
                    JwtSecurityToken jwt = _jwtService.Generate(user.Id);
                    AccessToken accessToken = new AccessToken
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                        Expiration = jwt.ValidTo
                    };
                    RefreshToken refreshToken = _jwtService.GenerateRefreshToken();
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = refreshToken.Expiration
                    };
                    Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);
                    _refreshToken.Token = refreshToken.Token;
                    _refreshToken.Expiration = refreshToken.Expiration;
                    _refreshToken.Created = refreshToken.Created;

                    JwtTokenDto jwtToken = new JwtTokenDto
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };

                    return Ok(jwtToken);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(int id)
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            if (!_refreshToken.Token.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if(_refreshToken.Expiration < DateTime.UtcNow)
            {
                return Unauthorized("Token expired.");
            }

            var jwt = _jwtService.Generate(id);
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(token);
        }

    }
}
