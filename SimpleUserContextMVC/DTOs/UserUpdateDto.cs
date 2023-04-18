using Microsoft.AspNetCore.Mvc;

namespace SimpleUser.MVC.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //[Remote(action: "IsUserAlreadyExists", controller: "Users")]
        public string Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
