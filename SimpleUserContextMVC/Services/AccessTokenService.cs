using Microsoft.AspNetCore.Http;

namespace SimpleUser.MVC.Services
{
    public interface IAccessTokenService
    {
        public string GetToken();
    }

    public class AccessTokenService : IAccessTokenService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public AccessTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["JWToken"];
        }
    }
}
