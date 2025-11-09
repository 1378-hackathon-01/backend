using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class InstitutionEntity : BaseModelEntity, IEntity<Institution>
{
    public string Abbreviation { get; set; }

    public string Title { get; set; }

    public InstitutionEntity(
        Guid id,
        string abbreviation,
        string title,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        Abbreviation = abbreviation;
        Title = title;
    }

    public InstitutionEntity(Institution model) : base(model)
    {
        Abbreviation = model.Abbreviation;
        Title = model.Title;
    }

    public new Institution GetModel()
    {
        return new Institution(
            id: Id,
            abbreviation: Abbreviation,
            title: Title,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
