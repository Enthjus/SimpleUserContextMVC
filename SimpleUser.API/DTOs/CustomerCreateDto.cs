namespace SimpleUser.API.DTOs
{
    public class CustomerCreateDto
    {
        public string Customername { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
