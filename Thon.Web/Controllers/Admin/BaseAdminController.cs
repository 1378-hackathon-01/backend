using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Core.Models;
using Thon.Web.Authorization;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Admin;

/// <summary>
/// Базовый контроллер для администраторских операций.
/// </summary>
public abstract class BaseAdminController(
    UserAdminService userAdminService,
    UserAdminAuthService userAdminAuthService) 
    : ControllerBase
{
    protected UserAdminService UserAdminService => userAdminService;

    protected UserAdminAuthService UserAdminAuthService => userAdminAuthService;

    private UserAdmin? _admin;
    private UserAdminAuth? _session;

    protected async Task<UserAdminAuth> GetSession()
    {
        if (_session is not null) return _session;

        if (User.Identity is not ClaimsIdentity claims)
            throw new ThonApiUnauthorizedException();

        var roleClaim = claims.FindFirst(AuthTokenClaim.Role);
        var sessionIdClaim = claims.FindFirst(AuthTokenClaim.SessionId);
        var sessionIdString = sessionIdClaim?.Value;

        if (roleClaim is null || roleClaim.Value != AuthTokenRole.Admin)
            throw new ThonApiUnauthorizedException();

        if (sessionIdString is null || !Guid.TryParse(sessionIdString, out var sessionId))
            throw new ThonApiUnauthorizedException();

        var session = await UserAdminAuthService.Get(id: sessionId);
        if (session is null) throw new ThonApiUnauthorizedException();

        _session = session;
        return session;
    }

    /// <summary>
    /// Получить текущего авторизованного администратора.
    /// Если администратор не авторизован или не существует, будет выброшено исключение.
    /// </summary>
    /// <returns>Авторизованный администратор.</returns>
    protected async Task<UserAdmin> GetUser()
    {
        if (_admin != null) return _admin;

        var session = await GetSession();
        var admin = await UserAdminService.Get(id: session.UserId);
        if (admin is null) throw new ThonApiUnauthorizedException();

        _admin = admin;
        return admin;
    }
}