using Microsoft.AspNetCore.Identity;
using SimpleUser.API.Auths;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Services
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpDto signUp);
        public Task<JwtToken> SignInAsync(SignInDto signIn);
    }
}
