using SimpleUser.API.Auths;

namespace SimpleUser.API.DTOs
{
    public class JwtTokenDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
