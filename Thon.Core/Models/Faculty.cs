namespace Thon.Core.Models;

/// <summary>
/// Факультет университета.
/// </summary>
public class Faculty : BaseInstitutionModel
{
    /// <summary>
    /// Аббревиатура названия факультета.
    /// </summary>
    public string Abbreviation { get; init; }

    /// <summary>
    /// Полное название факультета.
    /// </summary>
    public string Title { get; init; }

    public Faculty(
        Institution institution,
        string abbreviation,
        string title) 
        : base(institution)
    {
        Abbreviation = abbreviation;
        Title = title;
    }

    public Faculty(Faculty model) : base(model)
    {
        Abbreviation = model.Abbreviation;
        Title = model.Title;
    }

    public Faculty(
        Guid id,
        Guid institutionId,
        string abbreviation,
        string title,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            institutionId: institutionId,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        Abbreviation = abbreviation;
        Title = title;
    }
}
