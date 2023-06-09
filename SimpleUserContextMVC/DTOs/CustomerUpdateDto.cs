﻿namespace SimpleUser.MVC.DTOs
{
    public class CustomerUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
        public CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
