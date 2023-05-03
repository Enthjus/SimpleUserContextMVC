using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimpleUser.API.Auths;
using SimpleUser.API.DTOs;
using SimpleUser.API.Heplers;
using SimpleUser.Persistence.Data;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleUser.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<JwtToken> SignInAsync(SignInDto signInDto)
        {
            var result = await _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password, false, false);
            var user = await _signInManager.UserManager.FindByNameAsync(signInDto.Email);

            if (!result.Succeeded)
            {
                return null;
            }

            var token = await _jwtService.GenerateAsync(user);
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
