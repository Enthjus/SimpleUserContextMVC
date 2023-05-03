using SimpleUser.API.Auths;
using SimpleUser.Persistence.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleUser.API.Heplers
{
    public interface IJwtService
    {
        Task<JwtSecurityToken> GenerateAsync(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
