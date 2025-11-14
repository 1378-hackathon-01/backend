namespace Thon.Core.Models;

public class StudentRequestInstitutionFacultyGroup : BaseModel
{
    public Guid StudentRequestInstitutionFacultyId { get; }

    public Guid GroupId { get; }

    public StudentRequestInstitutionFacultyGroup(
        StudentRequestInstitutionFaculty studentRequestInstitutionFaculty,
        Group group)
    {
        StudentRequestInstitutionFacultyId = studentRequestInstitutionFaculty.Id;
        GroupId = group.Id;
    }

    public StudentRequestInstitutionFacultyGroup(StudentRequestInstitutionFacultyGroup model) : base(model)
    {
        StudentRequestInstitutionFacultyId = model.StudentRequestInstitutionFacultyId;
        GroupId = model.GroupId;
    }

    public StudentRequestInstitutionFacultyGroup(
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
}
