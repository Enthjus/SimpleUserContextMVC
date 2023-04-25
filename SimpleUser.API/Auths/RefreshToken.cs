namespace SimpleUser.API.Auths
{
    public class RefreshToken
    {
        public string Token { get; set; } 
        public DateTime Created { get; set; } 
        public DateTime Expiration { get; set; }
    }
}
