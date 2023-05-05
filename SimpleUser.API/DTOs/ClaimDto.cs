using System.Security.Claims;

namespace SimpleUser.API.DTOs
{
    public class ClaimDto
    {
        public string Email { get; set; }
        public List<Claim> Roles { get; set; }
    }
}
