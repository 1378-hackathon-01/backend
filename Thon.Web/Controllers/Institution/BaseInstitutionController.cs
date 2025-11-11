using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Thon.Core.Models;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Exceptions;

using InstitutionModel = Thon.Core.Models.Institution;

namespace Thon.Web.Controllers.Institution;

public class BaseInstitutionController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService) 
    : ControllerBase
{
    protected UserInstitutionService UserInstitutionService { get; } = userInstitutionService;

    protected UserInstitutionAuthService UserInstitutionAuthService { get; } = userInstitutionAuthService;

    protected InstitutionService InstitutionService { get; } = institutionService;

    private UserInstitution? _user;

    private UserInstitutionAuth? _session;

    private InstitutionModel? _institution;

    protected async Task<UserInstitutionAuth> GetSession()
    {
        if (_session is not null) return _session;

        if (User.Identity is not ClaimsIdentity claims)
            throw new ThonApiUnauthorizedException();

        var roleClaim = claims.FindFirst(AuthTokenClaim.Role);
        var sessionIdClaim = claims.FindFirst(AuthTokenClaim.SessionId);
        var sessionIdString = sessionIdClaim?.Value;

        if (roleClaim is null || roleClaim.Value != AuthTokenRole.Institution)
            throw new ThonApiUnauthorizedException();

        if (sessionIdString is null || !Guid.TryParse(sessionIdString, out var sessionId))
            throw new ThonApiUnauthorizedException();

        var session = await UserInstitutionAuthService.Get(id: sessionId);
        if (session is null) throw new ThonApiUnauthorizedException();

        _session = session;
        return session;
    }

    protected async Task<UserInstitution> GetUser()
    {
        if (_user is not null) return _user;

        var session = await GetSession();
        var user = await UserInstitutionService.Get(session.UserId);
        if (user is null) throw new ThonApiUnauthorizedException();

        _user = user;
        return user;
    }

    protected async Task<InstitutionModel> GetInstitution()
    {
        if (_institution is not null) return _institution;

        var user = await GetUser();
        var institution = await InstitutionService.Get(user.InstitutionId);
        if (institution is null) throw new ThonApiUnauthorizedException();

        _institution = institution;
        return institution;
    }
}
