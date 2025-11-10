using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Thon.App.Helpers;
using Thon.App.Services;
using Thon.Web.Services;

namespace Thon.Web.Authorization;

public static class ApiTokenPolicyExtensions
{
    public static AuthorizationPolicyBuilder RequireApiToken(this AuthorizationPolicyBuilder builder) => builder.RequireAssertion(IsAuthorized);

    private static async Task<bool> IsAuthorized(AuthorizationHandlerContext context)
    {
        if (context.Resource is not HttpContext httpContext) return false;
        if (!httpContext.Request.Headers.TryGetValue("API-Token", out var apiTokens)) return false;

        var apiToken = apiTokens.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(apiToken)) return false;

        var hasher = httpContext.RequestServices.GetRequiredService<Hasher>();
        var tokenCache = httpContext.RequestServices.GetRequiredService<ApiTokenCache>();
        var tokenService = httpContext.RequestServices.GetRequiredService<ApiTokenService>();

        var tokenHash = hasher.Sha256Salted(apiToken);
        var isTokenChecked = tokenCache.Get(tokenHash);

        if (!isTokenChecked)
        {
            var token = await tokenService.Get(tokenHash);

            if (token is not null)
            {
                tokenCache.Set(token.TokenHash);
                isTokenChecked = true;
            }
        }

        if (isTokenChecked)
        {
            var claims = new List<Claim> { new(AuthTokenClaim.ApiTokenHash, tokenHash) };
            var identity = new ClaimsIdentity(claims, AuthPolicies.Api);
            context.User.AddIdentity(identity);

            return true;
        }

        return false;
    }
}
