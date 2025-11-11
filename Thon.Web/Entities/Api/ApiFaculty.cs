using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiFaculty(Faculty faculty)
{
    public Guid Id { get; } = faculty.Id;

    public string Abbreviation { get; } = faculty.Abbreviation;

    public string Title { get; } = faculty.Title;
}
