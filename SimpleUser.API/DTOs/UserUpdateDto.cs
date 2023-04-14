using Microsoft.AspNetCore.Mvc;

namespace SimpleUser.API.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [Remote(action: "IsUserAlreadyExists", controller: "Users")]
        public string Email { get; set; }
        public string Password { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
