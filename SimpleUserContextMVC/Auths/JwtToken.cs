namespace SimpleUser.MVC.Auths
{
    public class JwtToken
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
