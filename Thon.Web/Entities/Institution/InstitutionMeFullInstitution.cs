using InstitutionModel = Thon.Core.Models.Institution;

namespace Thon.Web.Entities.Institution;

public class InstitutionMeFullInstitution(InstitutionModel institution)
{
    public Guid Id { get; } = institution.Id;

    public string Title { get; } = institution.Title;

    public string Abbreviation { get; } = institution.Abbreviation;
}
