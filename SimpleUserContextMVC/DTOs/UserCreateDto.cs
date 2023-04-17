using Microsoft.AspNetCore.Mvc;

namespace SimpleUser.MVC.DTOs
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
