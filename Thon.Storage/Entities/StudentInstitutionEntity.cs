using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentInstitutionEntity : BaseModelEntity, IEntity<StudentInstitution>
{
    public Guid StudentId { get; set; }

    public Guid InstitutionId { get; set; }

    public StudentInstitutionEntity(StudentInstitution model) : base(model)
    {
        StudentId = model.StudentId;
        InstitutionId = model.InstitutionId;
    }

    public StudentInstitutionEntity(
        Guid id,
        Guid studentId,
        Guid institutionId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        StudentId = studentId;
        InstitutionId = institutionId;
    }

    public new StudentInstitution GetModel()
    {
        return new StudentInstitution(
            id: Id,
            studentId: StudentId,
            institutionId: InstitutionId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
