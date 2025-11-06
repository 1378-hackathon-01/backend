using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Admin;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Admin;

[ApiController]
[Route("users")]
[Authorize(Policy = AuthPolicies.Admin)]
public class AdminUsersController(
    UserAdminService userAdminService,
    UserAdminAuthService userAdminAuthService)
    : BaseAdminController(
        userAdminService,
        userAdminAuthService)
{
    [HttpGet("me")]
    public async Task<AdminUserFull> GetMe()
    {
        var user = await GetUser();
        return new AdminUserFull(user);
    }

    [HttpPut("me")]
    public async Task Update([FromBody] AdminUserPut request)
    {
        ThonApiBadRequestException.ThrowIfNull(request);

        if (request.NewFullName is not null && request.NewFullName.Trim().Length < 4)
            throw new ThonApiBadRequestException("Invalid Full Name!");

        if (request.NewPassword is not null && request.NewPassword.Trim().Length < 4)
            throw new ThonApiBadRequestException("Invalid Password!");

        var user = await GetUser();

        await UserAdminService.Update(
            admin: user,
            newPassword: request.NewPassword,
            newFullName: request.NewFullName);
    }
}
