using Microsoft.AspNetCore.Identity;
using SimpleUser.API.Auths;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(SignUpDto signUp);
        Task<JwtToken> SignInAsync(SignInDto signIn);
        Task<IdentityResult> UpdateUserProfile(string email, UserProfileDto userProfile);
        Task<IdentityResult> ChangePassword(string email, ChangePasswordDto changePassword);
    }
}
