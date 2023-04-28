using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SimpleUser.API.Auths;
using SimpleUser.API.DTOs;
using SimpleUser.API.Heplers;
using SimpleUser.Persistence.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleUser.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<JwtToken> SignInAsync(SignInDto signInDto)
        {
            var result = await _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Email", signInDto.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:TokenValidityInMinutes")),
                signingCredentials: signIn);
            JwtToken jwtToken = new JwtToken
            {
                AccessToken = new AccessToken
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                },
                RefreshToken = _jwtService.GenerateRefreshToken()
            };
            return jwtToken;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpDto signUp)
        {
            var user = _mapper.Map<ApplicationUser>(signUp);

            return await _userManager.CreateAsync(user, signUp.Password);
        }
    }
}
