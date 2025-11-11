using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Api;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Api;

[ApiController]
[Authorize(Policy = AuthPolicies.Api)]
[Route("institutions")]
public class ApiInstitutionsController(
    InstitutionService institutionService, 
    FacultyService facultyService) 
    : ControllerBase
{
    [HttpGet()]
    public async Task<IReadOnlyList<ApiInstitution>> GetAll()
    {
        var institutions = await institutionService.Get();

        var response = institutions
            .Select(x => new ApiInstitution(x))
            .ToList();

        return response;
    }

    [HttpGet("{institutionId}/faculties")]
    public async Task<IReadOnlyList<ApiFaculty>> GetAll(Guid institutionId)
    {
        var institution = await institutionService.Get(institutionId);
        ThonApiNotFoundException.ThrowIfNull(institution, "Institution not found!");

        var faculties = await facultyService.Get(institution);

        return faculties
            .Select(x => new ApiFaculty(x))
            .ToList();
    }
}
