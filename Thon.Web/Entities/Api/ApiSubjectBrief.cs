using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiSubjectBrief(Subject subject)
{
    public Guid Id { get; } = subject.Id;

    public string Abbreviation { get; } = subject.Abbreviation;

    public string Title { get; } = subject.Title;
}
