namespace Thon.Core.Models;

public class Subject : BaseInstitutionModel
{
    public string Title { get; }

    public string Abbreviation { get; }

    public Subject(
        Institution institution,
        string title,
        string abbreviation)
        : base(
            institution: institution)
    {
        Title = title;
        Abbreviation = abbreviation;
    }

    public Subject(Subject model) : base(model)
    {
        Title = model.Title;
        Abbreviation = model.Abbreviation;
    }

    public Subject(
        Guid id,
        Guid institutionId,
        string title,
        string abbreviation,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            institutionId: institutionId,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        Title = title;
        Abbreviation = abbreviation;
    }
}
