using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentApprovedEntity : BaseModelEntity, IEntity<StudentApproved>
{
    public Guid InstitutionId { get; set; }

    public Guid FacultyId { get; set; }

    public Guid GroupId { get; set; }

    public Guid StudentId { get; set; }

    public StudentApprovedEntity(StudentApproved model) : base(model)
    {
        InstitutionId = model.InstitutionId;
        FacultyId = model.FacultyId;
        GroupId = model.GroupId;
        StudentId = model.StudentId;
    }

    public StudentApprovedEntity(
        Guid id,
        Guid institutionId,
        Guid facultyId,
        Guid groupId,
        Guid studentId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        InstitutionId = institutionId;
        FacultyId = facultyId;
        GroupId = groupId;
        StudentId = studentId;
    }

    public new StudentApproved GetModel()
    {
        return new StudentApproved(
            id: Id,
            institutionId: InstitutionId,
            facultyId: FacultyId,
            groupId: GroupId,
            studentId: StudentId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
