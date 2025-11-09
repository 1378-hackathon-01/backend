using Thon.Core.Models;

namespace Thon.Web.Entities.Admin;

public class AdminInstitutionBrief(Institution institution)
{
    public Guid Id { get; } = institution.Id;

    public string Abbreviation { get; } = institution.Abbreviation;

    public string Title { get; } = institution.Title;
}
