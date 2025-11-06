using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Thon.Web.Authorization;

namespace Thon.Web.Helpers;

public class AuthHelper(AuthOptions options)
{
    public string CreateJwtToken(AuthTokenRole role, Guid sessionId)
    {

        var jwt = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,

            claims: [
                new(AuthTokenClaim.SessionId.ToString(), sessionId.ToString()),
                new(AuthTokenClaim.Role.ToString(), role.ToString())],
            signingCredentials: new SigningCredentials(options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
