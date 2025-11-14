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
    FacultyService facultyService,
    GroupService groupService) 
    : ControllerBase
{
    [HttpGet()]
    public async Task<IReadOnlyList<ApiInstitution>> GetAllInstitutions()
    {
        var institutions = await institutionService.Get();

        var response = institutions
            .Select(x => new ApiInstitution(x))
            .ToList();

        return response;
    }

    [HttpGet("{institutionId}/faculties")]
    public async Task<IReadOnlyList<ApiFaculty>> GetAllInstitutionFaculties(Guid institutionId)
    {
        var institution = await institutionService.Get(institutionId);
        ThonApiNotFoundException.ThrowIfNull(institution, "Institution not found!");

        var faculties = await facultyService.Get(institution);

        return faculties
            .Select(x => new ApiFaculty(x))
            .ToList();
    }

    [HttpGet("{institutionId}/faculties/{facultyId}/groups")]
    public async Task<IReadOnlyList<ApiGroup>> GetAllFacultyGroups(Guid institutionId, Guid facultyId)
    {
        var institution = await institutionService.Get(institutionId);
        ThonApiNotFoundException.ThrowIfNull(institution, "Institution not found!");

        var faculty = await facultyService.Get(facultyId);
        ThonApiNotFoundException.ThrowIfNull(faculty, "Faculty not found!");

        if (institution.Id != faculty.InstitutionId)
            throw new ThonApiNotFoundException("Faculty not found in institution!");

        var groups = await groupService.Get(faculty);

        return groups
            .Select(x => new ApiGroup(x))
            .ToList();
    }
}
