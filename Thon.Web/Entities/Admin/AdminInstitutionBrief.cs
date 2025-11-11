using InstitutionModel = Thon.Core.Models.Institution;

namespace Thon.Web.Entities.Admin;

public class AdminInstitutionBrief(InstitutionModel institution)
{
    public Guid Id { get; } = institution.Id;

    public string Abbreviation { get; } = institution.Abbreviation;

    public string Title { get; } = institution.Title;
}
