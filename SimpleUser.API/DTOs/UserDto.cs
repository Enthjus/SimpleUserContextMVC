﻿namespace SimpleUser.API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}