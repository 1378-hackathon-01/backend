using Thon.Core.Models;

namespace Thon.Web.Entities.Institution;

public class InstitutionFacultyFull(Faculty faculty)
{
    public Guid Id { get; } = faculty.Id;

    public string Title { get; } = faculty.Title;

    public string Abbreviation { get; } = faculty.Abbreviation;
}
