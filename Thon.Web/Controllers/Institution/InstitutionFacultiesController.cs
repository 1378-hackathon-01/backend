using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Institution;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Institution;

[ApiController]
[Authorize(Policy = AuthPolicies.Institution)]
[Route("faculties")]
public class InstitutionFacultiesController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService,
    FacultyService facultyService)
    : BaseInstitutionController(
        userInstitutionService,
        userInstitutionAuthService,
        institutionService)
{
    [HttpGet()]
    public async Task<IReadOnlyList<InstitutionFacultyBrief>> Get()
    {
        var institution = await GetInstitution();
        var faculties = await facultyService.Get(institution);

        return faculties
            .Select(x => new InstitutionFacultyBrief(x))
            .ToList();
    }

    [HttpGet("{facultyId}")]
    public async Task<InstitutionFacultyFull> Get(Guid facultyId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        return new InstitutionFacultyFull(faculty);
    }

    [HttpPost]
    public async Task Create([FromBody] InstitutionFacultyPost request)
    {
        ThonApiBadRequestException.ThrowIfNull(request);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Title);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Abbreviation);

        var institution = await GetInstitution();

        await facultyService.Create(
            institution: institution,
            title: request.Title,
            abbreviation: request.Abbreviation);
    }

    [HttpDelete("{facultyId}")]
    public async Task Delete(Guid facultyId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        await facultyService.Delete(faculty);
    }
}
