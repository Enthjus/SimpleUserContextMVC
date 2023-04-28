namespace SimpleUser.API.Auths
{
    public class JwtToken
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
