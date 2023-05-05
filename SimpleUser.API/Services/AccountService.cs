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

        public async Task<IdentityResult> ChangePassword(string email, ChangePasswordDto changePassword)
        {
            var user = await FindByEmailAsync(email);
            return await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
        }

        public async Task<JwtToken> SignInAsync(SignInDto signInDto)
        {
            var user = await FindByEmailAsync(signInDto.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, signInDto.Password, false);
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

        public async Task<IdentityResult> UpdateUserProfile(string email, UserProfileDto userProfile)
        {
            var oldUser = await FindByEmailAsync(email);
            _mapper.Map(userProfile, oldUser);
            return await _userManager.UpdateAsync(oldUser);
        }

        private async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
    }
}
