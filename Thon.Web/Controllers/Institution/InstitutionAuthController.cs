using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Institution;
using Thon.Web.Exceptions;
using Thon.Web.Helpers;

namespace Thon.Web.Controllers.Institution;

[ApiController]
[Authorize(Policy = AuthPolicies.Institution)]
[Route("auth")]
public class InstitutionAuthController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService,
    AuthHelper authHelper)
    : BaseInstitutionController(
        userInstitutionService,
        userInstitutionAuthService,
        institutionService)
{
    [AllowAnonymous]
    [HttpGet("login")]
    public async Task<InstitutionAuthBrief> Login(
        [FromQuery] string login,
        [FromQuery] string password)
    {
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(login);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(password);

        login = login.Trim().ToLower();
        password = password.Trim();

        var user = await UserInstitutionService
            .Get(
                login: login,
                password: password);

        if (user is null)
            throw new ThonApiUnauthorizedException();

        var userAgent = HttpContext.Request.Headers.UserAgent.FirstOrDefault();
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

        var authSession = await UserInstitutionAuthService
            .Create(
                user: user,
                deviceUserAgent: userAgent,
                deviceIpAddress: ipAddress);

        var token = authHelper
            .CreateJwtToken(
                role: AuthTokenRole.Institution,
                sessionId: authSession.Id);

        return new InstitutionAuthBrief(token);
    }
}
