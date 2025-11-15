using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Institution;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Institution;

[ApiController]
[Authorize(Policy = AuthPolicies.Institution)]
[Route("subjects")]
public class InstitutionSubjectController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService,
    SubjectService subjectService)
    : BaseInstitutionController(
        userInstitutionService,
        userInstitutionAuthService,
        institutionService)
{
    [HttpGet()]
    public async Task<IReadOnlyList<InstitutionSubjectBrief>> Get()
    {
        var institution = await GetInstitution();
        var subjects = await subjectService.Get(institution);

        return subjects
            .Select(x => new InstitutionSubjectBrief(x))
            .ToList();
    }

    [HttpPost()]
    public async Task Create([FromBody] InstitutionSubjectPost request)
    {
        ThonApiBadRequestException.ThrowIfNull(request);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Title);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Abbreviation);

        var institution = await GetInstitution();

        await subjectService.Create(
            institution: institution,
            title: request.Title,
            abbreviation: request.Abbreviation);
    }

    [HttpDelete("{subjectId}")]
    public async Task Delete(Guid subjectId)
    {
        var institution = await GetInstitution();
        var subject = await subjectService.Get(subjectId);

        if (subject is null || subject.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Subject not found!");

        await subjectService.Delete(subject);
    }
}
