using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentRequestInstitutionEntity : BaseModelEntity, IEntity<StudentRequestInstitution>
{
    public Guid StudentId { get; set; }

    public Guid InstitutionId { get; set; }

    public StudentRequestInstitutionEntity(StudentRequestInstitution model) : base(model)
    {
        StudentId = model.StudentId;
        InstitutionId = model.InstitutionId;
    }

    public StudentRequestInstitutionEntity(
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

    public new StudentRequestInstitution GetModel()
    {
        return new StudentRequestInstitution(
            id: Id,
            studentId: StudentId,
            institutionId: InstitutionId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
