using SimpleUser.API.Validators;

namespace SimpleUser.API.DTOs
{
    public class UserDetailDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
