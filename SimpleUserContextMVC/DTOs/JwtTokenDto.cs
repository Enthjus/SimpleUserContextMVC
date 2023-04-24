namespace SimpleUser.MVC.DTOs
{
    public class JwtTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationRefreshToken { get; set; }
    }
}
