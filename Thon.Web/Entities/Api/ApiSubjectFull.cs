using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiSubjectFull(Subject subject, GroupSubject groupSubject)
{
    public Guid Id { get; } = subject.Id;

    public string Title { get; } = subject.Title;

    public string Abbreviation { get; } = subject.Abbreviation;

    public string? Content { get; } = groupSubject.Content;
}
