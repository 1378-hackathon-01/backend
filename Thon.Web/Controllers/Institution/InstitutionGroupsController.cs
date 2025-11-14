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
public class InstitutionGroupsController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService,
    FacultyService facultyService,
    GroupService groupService)
    : BaseInstitutionController(
        userInstitutionService,
        userInstitutionAuthService,
        institutionService)
{
    [HttpGet("{facultyId}/groups/{groupId}")]
    public async Task<InstitutionGroupBrief> Get(Guid facultyId, Guid groupId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        return new InstitutionGroupBrief(group);
    }

    [HttpGet("{facultyId}/groups")]
    public async Task<IReadOnlyList<InstitutionGroupBrief>> Get(Guid facultyId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        var groups = await groupService.Get(faculty);

        return groups
            .Select(x => new InstitutionGroupBrief(x))
            .ToList();
    }

    [HttpPost("{facultyId}/groups")]
    public async Task Create(Guid facultyId, [FromBody] InstitutionGroupPost request)
    {
        ThonApiBadRequestException.ThrowIfNull(request);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Title);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Abbreviation);

        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        await groupService.Create(
            faculty: faculty,
            title: request.Title.Trim(),
            abbreviation: request.Abbreviation.Trim());
    }

    [HttpDelete("{facultyId}/groups/{groupId}")]
    public async Task Delete(Guid facultyId, Guid groupId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        await groupService.Delete(group);
    }
}
