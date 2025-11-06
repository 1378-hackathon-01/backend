using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Admin;
using Thon.Web.Exceptions;
using Thon.Web.Helpers;

namespace Thon.Web.Controllers.Admin;

[ApiController]
[Route("auth")]
[Authorize(Policy = AuthPolicies.Admin)]
public class AdminAuthController(
    UserAdminService userAdminService,
    UserAdminAuthService userAdminAuthService,
    AuthHelper authHelper) 
    : BaseAdminController(
        userAdminService, 
        userAdminAuthService)
{
    [HttpGet("login")]
    [AllowAnonymous]
    public async Task<AdminAuthBrief> Login(
        [FromQuery] string login,
        [FromQuery] string password)
    {
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(login);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(password);

        login = login.Trim().ToLower();
        password = password.Trim();

        var admin = await UserAdminService
            .Get(
                login: login,
                password: password);

        if (admin is null) 
            throw new ThonApiUnauthorizedException();

        var userAgent = HttpContext.Request.Headers.UserAgent.FirstOrDefault();
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

        var authSession = await UserAdminAuthService
            .CreateAuth(
                userAdmin: admin,
                deviceUserAgent: userAgent,
                deviceIpAddress: ipAddress);

        var token = authHelper
            .CreateJwtToken(
                role: AuthTokenRole.Admin, 
                sessionId: authSession.Id);

        return new AdminAuthBrief(token);
    }

    [HttpGet("")]
    public async Task<AdminAuthFull> Get()
    {
        var session = await GetSession();
        return new AdminAuthFull(session);
    }

    [HttpGet("list")]
    public async Task<IReadOnlyList<AdminAuthFull>> GetAll()
    {
        var session = await GetSession();
        var admin = await GetUser();

        var sessions = await UserAdminAuthService.Get(admin: admin);

        var filteredSessions = sessions
            .Where(x => x.Id != session.Id)
            .Select(x => new AdminAuthFull(x))
            .ToList();

        return filteredSessions;
    }

    [HttpDelete("logout")]
    public async Task Logout()
    {
        var session = await GetSession();

        await UserAdminAuthService.Delete(session);
    }

    [HttpDelete("logout/{sessionId}")]
    public async Task Logout(Guid sessionId)
    {
        var session = await GetSession();
        var sessionToDelete = await UserAdminAuthService.Get(id: sessionId);

        if (sessionToDelete is null || sessionToDelete.UserId != session.UserId)
            throw new ThonApiNotFoundException();

        await UserAdminAuthService.Delete(sessionToDelete);
    }
}
