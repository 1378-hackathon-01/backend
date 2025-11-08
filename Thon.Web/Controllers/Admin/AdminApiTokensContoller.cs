using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.Core.Enums;
using Thon.App.Services;
using Thon.Web.Exceptions;
using Thon.Web.Authorization;
using Thon.Web.Entities.Admin;

namespace Thon.Web.Controllers.Admin;

[ApiController]
[Route("tokens")]
[Authorize(Policy = AuthPolicies.Admin)]
public class AdminApiTokensContoller(
    ApiTokenService apiTokenService,
    UserAdminService userAdminService,
    UserAdminAuthService userAdminAuthService)
    : BaseAdminController(
        userAdminService,
        userAdminAuthService)
{
    [HttpGet("")]
    public async Task<IReadOnlyList<AdminApiTokenFull>> Get()
    {
        var user = await GetUser();
        ThonApiForbiddenException.ThrowIfLess(user.AccessApiTokens, AccessLevel.Read);

        var tokens = await apiTokenService.Get();

        var response = tokens
            .Select(x => new AdminApiTokenFull(x))
            .ToList();

        return response;
    }

    [HttpPost("")]
    public async Task<AdminApiTokenPostFull> Create()
    {
        var user = await GetUser();
        ThonApiForbiddenException.ThrowIfLess(user.AccessApiTokens, AccessLevel.Write);

        var tokenCreationResult = await apiTokenService.Create();
        return new AdminApiTokenPostFull(tokenCreationResult);
    }

    [HttpDelete("{tokenId}")]
    public async Task Delete(Guid tokenId)
    {
        var user = await GetUser();

        ThonApiForbiddenException.ThrowIfLess(user.AccessApiTokens, AccessLevel.Write);

        var token = await apiTokenService.Get(id: tokenId);
        ThonApiNotFoundException.ThrowIfNull(token);

        await apiTokenService.Delete(token);
    }
}
