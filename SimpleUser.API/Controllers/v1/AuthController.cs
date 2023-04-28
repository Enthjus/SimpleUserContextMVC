using Microsoft.AspNetCore.Mvc;
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
        private ICustomerService _CustomerService;
        private IJwtService _jwtService;
        public IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthController(ICustomerService CustomerService, IJwtService jwtService, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _CustomerService = CustomerService;
            _jwtService = jwtService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInDto userLogin)
        {
            if (userLogin != null && userLogin.Email != null && userLogin.Password != null)
            {
                Customer Customer = await _CustomerService.Login(userLogin.Email, userLogin.Password);

                if (Customer != null)
                {
                    JwtSecurityToken jwt = _jwtService.Generate(Customer.Customername);
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

                    JwtToken jwtToken = new JwtToken
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };
                    _logger.LogInformation($"Login to Customer {Customer.Customername}");
                    return Ok(jwtToken);
                }
                else
                {
                    _logger.LogInformation("Customername or password is not true");
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                _logger.LogInformation("Customername or password is null");
                return BadRequest();
            }
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(string username)
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            if (!_refreshToken.Token.Equals(refreshToken))
            {
                _logger.LogInformation("Refresh token is not match");
                return Unauthorized("Invalid Refresh Token.");
            }
            else if(_refreshToken.Expiration < DateTime.UtcNow)
            {
                _logger.LogInformation("Token expired");
                return Unauthorized("Token expired.");
            }

            var jwt = _jwtService.Generate(username);
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            _logger.LogInformation("Successful regenerate token");
            return Ok(token);
        }

    }
}
