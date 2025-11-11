using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Institution;

namespace Thon.Web.Controllers.Institution;

[ApiController]
[Authorize(Policy = AuthPolicies.Institution)]
[Route("users")]
public class InstitutionUsersController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService)
    : BaseInstitutionController(
        userInstitutionService,
        userInstitutionAuthService, 
        institutionService)
{
    [HttpGet("me")]
    public async Task<InstitutionMeFull> GetMe()
    {
        var user = await GetUser();
        var institution = await GetInstitution();

        return new InstitutionMeFull(user, institution);
    }
}
