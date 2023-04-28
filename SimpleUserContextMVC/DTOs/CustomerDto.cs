namespace SimpleUser.MVC.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
