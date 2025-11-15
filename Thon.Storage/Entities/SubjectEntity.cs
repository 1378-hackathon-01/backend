using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class SubjectEntity : BaseInstitutionModelEntity, IEntity<Subject>
{
    public string Title { get; set; }

    public string Abbreviation { get; set; }

    public SubjectEntity(Subject model) : base(model)
    {
        Title = model.Title;
        Abbreviation = model.Abbreviation;
    }

    public SubjectEntity(
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

    public new Subject GetModel()
    {
        return new Subject(
            id: Id,
            institutionId: InstitutionId,
            title: Title,
            abbreviation: Abbreviation,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
