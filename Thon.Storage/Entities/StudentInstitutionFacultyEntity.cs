using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentInstitutionFacultyEntity : BaseModelEntity, IEntity<StudentInstitutionFaculty>
{
    public Guid StudentInstitutionId { get; set; }

    public Guid FacultyId { get; set; }

    public StudentInstitutionFacultyEntity(StudentInstitutionFaculty model) : base(model)
    {
        StudentInstitutionId = model.StudentInstitutionId;
        FacultyId = model.FacultyId;
    }

    public StudentInstitutionFacultyEntity(
        Guid id,
        Guid studentInstitutionId,
        Guid facultyId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        StudentInstitutionId = studentInstitutionId;
        FacultyId = facultyId;
    }

    public new StudentInstitutionFaculty GetModel()
    {
        return new StudentInstitutionFaculty(
            id: Id,
            studentInstitutionId: StudentInstitutionId,
            facultyId: FacultyId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
