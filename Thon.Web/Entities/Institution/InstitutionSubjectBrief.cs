using Thon.Core.Models;

namespace Thon.Web.Entities.Institution;

public class InstitutionSubjectBrief(Subject subject)
{
    public Guid Id { get; } = subject.Id;

    public string Title { get; } = subject.Title;

    public string Abbreviation { get; } = subject.Abbreviation;
}
