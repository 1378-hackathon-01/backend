using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiGroup(Group group)
{
    public Guid Id { get; } = group.Id;

    public string Title { get; } = group.Title;

    public string Abbreviation { get; } = group.Abbreviation;
}
