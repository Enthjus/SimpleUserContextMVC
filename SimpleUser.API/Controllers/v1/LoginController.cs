using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SimpleUser.API.DTOs;
using SimpleUser.API.Heplers;
using SimpleUser.API.Services;
using SimpleUser.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleUser.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : Controller
    {
        private IUserService _userService;
        private IJwtService _jwtService;
        public IConfiguration _configuration;

        public LoginController(IUserService userService, IJwtService jwtService, IConfiguration configuration)
        {
            _userService = userService;
            _jwtService = jwtService;   
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            if(userLogin != null && userLogin.Email != null && userLogin.Password != null)
            {
                User user = await _userService.Login(userLogin.Email, userLogin.Password);

                if (user != null)
                {
                    JwtSecurityToken jwt = _jwtService.Generate(user.Id, user.Username);

                    JwtTokenDto jwtToken = new JwtTokenDto
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                        ExpirationToken = jwt.ValidTo,
                        RefreshToken = _jwtService.GenerateRefreshToken(),
                        ExpirationRefreshToken = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshTokenValidityInDays"))
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
    }
}
