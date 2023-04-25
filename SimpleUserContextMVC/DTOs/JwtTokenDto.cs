using SimpleUser.MVC.Auths;

namespace SimpleUser.MVC.DTOs
{
    public class JwtTokenDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
