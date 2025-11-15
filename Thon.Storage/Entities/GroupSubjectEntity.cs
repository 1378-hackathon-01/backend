using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class GroupSubjectEntity : BaseModelEntity, IEntity<GroupSubject>
{
    public Guid GroupId { get; set; }

    public Guid SubjectId { get; set; }

    public string? Content { get; set; }

    public GroupSubjectEntity(GroupSubject model) : base(model)
    {
        GroupId = model.GroupId;
        SubjectId = model.SubjectId;
        Content = model.Content;
    }

    public GroupSubjectEntity(
        Guid id,
        Guid groupId,
        Guid subjectId,
        string? content,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        GroupId = groupId;
        SubjectId = subjectId;
        Content = content;
    }

    public new GroupSubject GetModel()
    {
        return new GroupSubject(
            id: Id,
            groupId: GroupId,
            subjectId: SubjectId,
            content: Content,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
