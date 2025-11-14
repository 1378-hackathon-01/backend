using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class GroupEntity : BaseModelEntity, IEntity<Group>
{
    public Guid FacultyId { get; set; }

    public string Title { get; set; }

    public string Abbreviation { get; set; }

    public GroupEntity(Group model) : base(model)
    {
        FacultyId = model.FacultyId;
        Title = model.Title;
        Abbreviation = model.Abbreviation;
    }

    public GroupEntity(
        Guid id,
        Guid facultyId,
        string title,
        string abbreviation,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        FacultyId = facultyId;
        Title = title;
        Abbreviation = abbreviation;
    }

    public new Group GetModel()
    {
        return new Group(
            id: Id,
            facultyId: FacultyId,
            title: Title,
            abbreviation: Abbreviation,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
