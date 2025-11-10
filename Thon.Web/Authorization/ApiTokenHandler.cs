using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Thon.App.Helpers;
using Thon.App.Services;
using Thon.Web.Services;

namespace Thon.Web.Authorization;

public class ApiTokenHandler(
    ApiTokenCache apiTokenCache,
    ApiTokenService apiTokenService,
    Hasher hasher) 
    : AuthorizationHandler<ApiTokenRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        ApiTokenRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("API-Token", out var apiTokens))
            {
                var apiToken = apiTokens.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(apiToken)) 
                {
                    var tokenHash = hasher.Sha256Salted(apiToken);

                    var isTokenChecked = apiTokenCache.Get(tokenHash);

                    if (!isTokenChecked)
                    {
                        var token = await apiTokenService.Get(tokenHash);
                        
                        if (token is not null)
                        {
                            apiTokenCache.Set(token.TokenHash);
                            isTokenChecked = true;
                        }
                    }

                    if (isTokenChecked)
                    {
                        var claims = new List<Claim> { new(AuthTokenClaim.ApiTokenHash, tokenHash) };
                        var identity = new ClaimsIdentity(claims, AuthPolicies.Api);
                        context.User.AddIdentity(identity);

                        context.Succeed(requirement);
                    }
                }
            }
        }
    }
}
