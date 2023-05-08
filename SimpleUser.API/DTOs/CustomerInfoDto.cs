namespace SimpleUser.API.DTOs
{
    public class CustomerInfoDto
    {
        public int Id { get; set; }
        public string Customername { get; set; }
        public string Email { get; set; }
        public virtual CustomerDetailDto CustomerDetailDto { get; set; }
    }
}
