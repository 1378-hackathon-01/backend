using Thon.Core.Models;

namespace Thon.Web.Entities.Institution;

public class InstitutionGroupSubjectFull(Subject subject, GroupSubject groupSubject)
{
    public Guid Id { get; } = subject.Id;

    public string Title { get; } = subject.Title;

    public string Abbreviation { get; } = subject.Abbreviation;

    public string? Content { get; } = groupSubject.Content;
}
