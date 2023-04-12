namespace SimpleUser.MVC.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }// TODO: only use fluent validation or data annotation
        public string Email { get; set; }
        public string Password { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
