using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.Validators;

namespace SimpleUser.API.DTOs
{
    public class UserCreateDto : BaseValidationModel<UserCreateDto>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
