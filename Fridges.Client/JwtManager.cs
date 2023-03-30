using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fridges.Client;

public static class JwtManager
{
    public static bool IsAllowed(HttpContext context, string role)
    {
        var jwtToken = context.Session.GetString("jwtToken");
        if(String.IsNullOrEmpty(jwtToken))
        {
            return false;
        }

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtHandler.ReadToken(jwtToken) as JwtSecurityToken;
        var claims = tokenContent.Claims.Where(claim => claim.Type == ClaimTypes.Role);
        var roles = claims.Select(claim => claim.Value);

        return roles.Contains(role);
    }
}
