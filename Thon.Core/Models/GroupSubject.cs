namespace Thon.Core.Models;

public class GroupSubject : BaseModel
{
    public Guid GroupId { get; }

    public Guid SubjectId { get; }

    public string? Content { get; init; }

    public GroupSubject(Group group, Subject subject)
    {
        GroupId = group.Id;
        SubjectId = subject.Id;
    }

    public GroupSubject(GroupSubject model) : base(model)
    {
        GroupId = model.GroupId;
        SubjectId = model.SubjectId;
        Content = model.Content;
    }

    public GroupSubject(
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

}
