namespace SimpleUser.API.DTOs
{
    public class CustomerUpdateDto
    {
        public int Id { get; set; }
        public string Customername { get; set; }
        public string Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
