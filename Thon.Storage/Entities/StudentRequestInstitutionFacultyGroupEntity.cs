using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentRequestInstitutionFacultyGroupEntity : BaseModelEntity, IEntity<StudentRequestInstitutionFacultyGroup>
{
    public Guid StudentRequestInstitutionFacultyId { get; set; }

    public Guid GroupId { get; set; }

    public StudentRequestInstitutionFacultyGroupEntity(StudentRequestInstitutionFacultyGroup model) : base(model)
    {
        StudentRequestInstitutionFacultyId = model.StudentRequestInstitutionFacultyId;
        GroupId = model.GroupId;
    }

    public StudentRequestInstitutionFacultyGroupEntity(
        Guid id,
        Guid studentRequestInstitutionFacultyId,
        Guid groupId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        StudentRequestInstitutionFacultyId = studentRequestInstitutionFacultyId;
        GroupId = groupId;
    }

    public new StudentRequestInstitutionFacultyGroup GetModel()
    {
        return new StudentRequestInstitutionFacultyGroup(
            id: Id,
            studentRequestInstitutionFacultyId: StudentRequestInstitutionFacultyId,
            groupId: GroupId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
