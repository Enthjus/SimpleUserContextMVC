using SimpleUser.API.Auths;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleUser.API.Heplers
{
    public interface IJwtService
    {
        JwtSecurityToken Generate(string username);
        RefreshToken GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
