using Microsoft.AspNetCore.Mvc;

namespace SimpleUser.API.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? NewPassword { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
