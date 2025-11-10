using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Thon.Web.Authorization;

public class BearerTokenHandler : TokenHandler
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public override Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
    {
        try
        {
            // Есть метод ValidateTokenAsync, но на текущей версии библиотеки Microsoft.AspNetCore.Authentication.JwtBearer 8.0.1
            // его использовать нельзя, потому что он сломан.
            // Судя по всему, он зависит от устаревшей версии библиотеки System.Text, которая не позволяет корректно разложить токен
            // на составные части header.payload.signature, из-за чего валидация корректного токена проваливается.

            _tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                return Task.FromResult(new TokenValidationResult { IsValid = false });

            return Task.FromResult(new TokenValidationResult
            {
                IsValid = true,
                ClaimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims, JwtBearerDefaults.AuthenticationScheme),
                SecurityToken = jwtSecurityToken,
            });
        }

        catch (Exception e)
        {
            return Task.FromResult(new TokenValidationResult
            {
                IsValid = false,
                Exception = e,
            });
        }
    }
}
