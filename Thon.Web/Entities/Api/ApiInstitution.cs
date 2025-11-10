using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiInstitution(Institution institution)
{
    public Guid Id { get; } = institution.Id;

    public string Title { get; } = institution.Title;

    public string Abbreviation { get; } = institution.Abbreviation;
}
