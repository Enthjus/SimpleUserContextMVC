namespace SimpleUser.API.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Customername { get; set; }
        public string Email { get; set; }
        public CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
