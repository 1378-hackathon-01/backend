using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentRequestInstitutionFacultyEntity : BaseModelEntity, IEntity<StudentRequestInstitutionFaculty>
{
    public Guid StudentRequestInstitutionId { get; set; }

    public Guid FacultyId { get; set; }

    public StudentRequestInstitutionFacultyEntity(StudentRequestInstitutionFaculty model) : base(model)
    {
        StudentRequestInstitutionId = model.StudentRequestInstitutionId;
        FacultyId = model.FacultyId;
    }

    public StudentRequestInstitutionFacultyEntity(
        Guid id,
        Guid studentRequestInstitutionId,
        Guid facultyId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        StudentRequestInstitutionId = studentRequestInstitutionId;
        FacultyId = facultyId;
    }

    public new StudentRequestInstitutionFaculty GetModel()
    {
        return new StudentRequestInstitutionFaculty(
            id: Id,
            studentRequestInstitutionId: StudentRequestInstitutionId,
            facultyId: FacultyId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
