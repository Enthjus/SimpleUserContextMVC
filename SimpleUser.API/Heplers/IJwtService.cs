using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleUser.API.Heplers
{
    public interface IJwtService
    {
        JwtSecurityToken Generate(int id, string username);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
