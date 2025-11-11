using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class FacultyEntity : BaseInstitutionModelEntity, IEntity<Faculty>
{
    public string Abbreviation { get; set; }

    public string Title { get; set; }

    public FacultyEntity(Faculty model) : base(model)
    {
        Abbreviation = model.Abbreviation;
        Title = model.Title;
    }

    public FacultyEntity(
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

    public new Faculty GetModel()
    {
        return new Faculty(
            id: Id,
            institutionId: InstitutionId,
            abbreviation: Abbreviation,
            title: Title,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
