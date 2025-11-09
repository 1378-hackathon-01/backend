using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Core.Enums;
using Thon.Web.Authorization;
using Thon.Web.Entities.Admin;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Admin;

[ApiController]
[Route("institutions")]
[Authorize(Policy = AuthPolicies.Admin)]
public class AdminInstitutionsController(
    UserAdminService userAdminService,
    UserAdminAuthService userAdminAuthService,
    InstitutionService institutionService,
    UserInstitutionService userInstitutionService)
    : BaseAdminController(
        userAdminService,
        userAdminAuthService)
{
    [HttpGet()]
    public async Task<IReadOnlyList<AdminInstitutionBrief>> Get()
    {
        var user = await GetUser();

        ThonApiForbiddenException.ThrowIfLess(user.AccessInstitutions, AccessLevel.Read);

        var institutions = await institutionService.Get();

        var response = institutions
            .Select(x => new AdminInstitutionBrief(x))
            .ToList();

        return response;
    }

    [HttpPost()]
    public async Task<AdminInstitutionPostFull> Create([FromBody] AdminInstitutionPost request)
    {
        ThonApiBadRequestException.ThrowIfNull(request);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Title);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Abbreviation);

        var user = await GetUser();

        ThonApiForbiddenException.ThrowIfLess(user.AccessInstitutions, AccessLevel.Write);

        var title = request.Title.Trim();
        var abbreviation = request.Abbreviation.Trim();

        var institution = await institutionService.Create(
            title: title,
            abbreviation: abbreviation);

        var userLogin = GenerateLogin();
        var userPassword = GeneratePassword();

        await userInstitutionService.Create(
            institution: institution,
            login: userLogin,
            password: userPassword,
            fullName: "Главный Администратор");

        return new AdminInstitutionPostFull(
            login: userLogin,
            password: userPassword);
    }

    [HttpDelete("{institutionId}")]
    public async Task Delete(Guid institutionId)
    {
        var user = await GetUser();

        ThonApiForbiddenException.ThrowIfLess(user.AccessInstitutions, AccessLevel.Write);

        var institution = await institutionService.Get(institutionId);
        ThonApiNotFoundException.ThrowIfNull(institution);

        await institutionService.Delete(institution);
    }

    private static string GenerateLogin()
    {
        var part1 = "admin";
        var part2 = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var part3 = Guid.NewGuid().ToString().ToLower().Replace("-", "");
        return string.Join("_", part1, part2, part3);
    }

    private static string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_!?()*";
        
        var password = new string(
            Enumerable
                .Repeat(chars, 16)
                .Select(s => s[Random.Shared.Next(s.Length)])
                .ToArray());

        return password;
    }
}
