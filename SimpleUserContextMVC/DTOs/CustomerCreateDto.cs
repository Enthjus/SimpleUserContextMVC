namespace SimpleUser.MVC.DTOs
{
    public class CustomerCreateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
