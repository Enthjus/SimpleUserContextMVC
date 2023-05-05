using SimpleUser.MVC.Auths;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;

namespace SimpleUser.MVC.Services
{
    public interface IUserService
    {
        Task<JwtToken> SignInAsync(LoginViewModel model);
    }
}
